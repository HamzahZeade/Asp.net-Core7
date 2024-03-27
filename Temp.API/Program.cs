using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Temp.API.Extensions;
using Temp.DAL.Data;
using Temp.DAL.Extensions;
using Temp.DAL.Seeds;
using Temp.Services.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Db Con ..........
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
#endregion Db Con ..........

#region Repo & UOW ..........
builder.Services.ServiceDependencies();
builder.Services.InfraDependencies(builder.Configuration);
#endregion Repo & UOW ..........
#region Mapster Mapper ..........
TypeAdapterConfig typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
string dynamicallyLoadedAssemblyPath = Path.Combine(AppContext.BaseDirectory, "Temp.Services.dll");
Assembly servicesAssembly = Assembly.Load(AssemblyName.GetAssemblyName(dynamicallyLoadedAssemblyPath));
typeAdapterConfig.Scan(servicesAssembly);
builder.Services.AddSingleton<IMapper>(a => new Mapper(typeAdapterConfig));
#endregion Mapster Mapper ..........
#region Jwt
builder.Services.AddJwtAuthentication(builder.Configuration);
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();
// Seed data before running the application
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Seed the data
    DataSeeder.SeedAsync(context).Wait(); // Make sure to handle async correctly in a production application
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

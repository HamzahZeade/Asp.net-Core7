using Microsoft.Extensions.DependencyInjection;
using Temp.Services.AuthServices;
using Temp.Services.AutoMapper;
using Temp.Services.EmployeesTasksService;
using Temp.Services.SpeakersService;

namespace Temp.Services.Extensions
{
    public static class ServiceDependenciesExtensions
    {
        public static IServiceCollection ServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISpeakersServices, SpeakersServices>();
            services.AddScoped<IEmployeeTasksService, EmployeeTasksService>();

            services.AddAutoMapper(typeof(ServiceDependenciesExtensions), typeof(AutoMapperProfile));
            //TypeAdapterConfig typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            //string dynamicallyLoadedAssemblyPath = Path.Combine(AppContext.BaseDirectory, "Temp.Services.dll");
            //Assembly servicesAssembly = Assembly.Load(AssemblyName.GetAssemblyName(dynamicallyLoadedAssemblyPath));
            //typeAdapterConfig.Scan(servicesAssembly);
            //services.AddSingleton<IMapper>(a => new Mapper(typeAdapterConfig));







            return services;
        }
    }
}

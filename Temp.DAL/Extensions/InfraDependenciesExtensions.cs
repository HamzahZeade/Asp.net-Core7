using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Temp.DAL.Cache;
using Temp.DAL.Interfaces;
using Temp.DAL.Repositories;
using Temp.DAL.UnitOfWorks;

namespace Temp.DAL.Extensions
{
    public static class InfraDependenciesExtensions
    {
        public static IServiceCollection InfraDependencies(this IServiceCollection services, IConfiguration _configuration)
        {

            services.TryAddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            return services;

        }
    }
}

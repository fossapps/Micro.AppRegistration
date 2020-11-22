using Micro.AppRegistration.Models;
using Micro.AppRegistration.Repository;
using Micro.AppRegistration.Uuid;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddSingleton<IUuidService, UuidService>();
        }
    }
}
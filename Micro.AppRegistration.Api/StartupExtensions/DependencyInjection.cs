using Micro.AppRegistration.Api.Models;
using Micro.AppRegistration.Api.Uuid;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
            services.AddSingleton<IUuidService, UuidService>();
        }
    }
}
using Micro.AppRegistration.Api.Internal.Configs;
using Micro.AppRegistration.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.Internal.StartupExtensions
{
    public static class Configuration
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("DatabaseConfig"));
            services.Configure<SlackLoggingConfig>(configuration.GetSection("Logging").GetSection("Slack"));
            services.Configure<Services>(configuration.GetSection("Services"));
        }
    }
}

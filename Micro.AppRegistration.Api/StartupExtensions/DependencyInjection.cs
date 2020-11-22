using System;
using Fossapps.Micro.KeyStore;
using Micro.AppRegistration.Api.Auth;
using Micro.AppRegistration.Api.Configs;
using Micro.AppRegistration.Api.Models;
using Micro.AppRegistration.Api.Uuid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>();
            services.AddSingleton<IUuidService, UuidService>();
            services.AddSingleton<IKeyResolver, KeyResolver>();
            services.AddSingleton(SetupKeyStoreHttpClient(configuration.GetSection("Services").Get<Services>().KeyStore));
        }

        private static IKeyStoreClient SetupKeyStoreHttpClient(KeyStoreConfig config)
        {
            return new KeyStoreClient
            {
                BaseUri = new Uri(config.Url)
            };
        }
    }
}

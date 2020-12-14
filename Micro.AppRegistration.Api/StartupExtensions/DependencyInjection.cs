using System;
using Fossapps.Micro.KeyStore;
using Micro.AppRegistration.Api.Auth;
using Micro.AppRegistration.Api.Configs;
using Micro.AppRegistration.Api.CreateApplication;
using Micro.AppRegistration.Api.ListApplications;
using Micro.AppRegistration.Api.Models;
using Micro.AppRegistration.Api.Uuid;
using Micro.AppRegistration.Api.VerifySecret;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IAppRegistrationService, AppRegistrationService>();
            services.AddScoped<ICreateApplicationRepository, CreateApplicationRepository>();
            services.AddScoped<IListApplicationRepository, ListApplicationRepository>();
            services.AddScoped<IListApplicationService, ListApplicationsService>();
            services.AddScoped<IVerifySecretService, VerifySecretService>();
            services.AddSingleton<IPasswordHasher<Application>, PasswordHasher<Application>>();
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

using System;
using Fossapps.Micro.KeyStore;
using Micro.AppRegistration.Api.GraphQL.Directives;
using Micro.AppRegistration.Api.Internal.Configs;
using Micro.AppRegistration.Business.CreateApplication;
using Micro.AppRegistration.Business.ListApplication;
using Micro.AppRegistration.Business.Verification;
using Micro.AppRegistration.Common;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.Internal.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationContext>();
            services.AddSingleton<IUuidService, UuidService>();
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

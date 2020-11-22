using System;
using System.IO;
using Micro.AppRegistration.Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PemUtils;

namespace Micro.AppRegistration.Api.StartupExtensions
{
    public static class Identity
    {
        public static void ConfigureIdentityServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();
        }

        private static void ConfigureJwtBearer(IServiceCollection services, JwtBearerOptions config)
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = false;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateActor = true,
                ValidateAudience = true,
                ValidIssuer = "my_app_auth",
                ValidAudiences = new []{"my_app"},
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKeyResolver = (token, secToken, kid, parameters) =>
                {
                    // todo: I know this .Result is a very bad idea (converting from async to sync)
                    // however there's no other way to do this, signing key resolver doesn't have a
                    // async version of this method, they are looking into it though
                    // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/468
                    var key = services.BuildServiceProvider().GetRequiredService<IKeyResolver>()
                        .ResolveKey(kid).Result;
                    var pemReader = new PemReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(key)));
                    var publicKeyParameters = pemReader.ReadRsaKey();
                    return new []{new RsaSecurityKey(publicKeyParameters)};                }
            };
        }
    }
}

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
                    // todo: first get keystore to generate a sdk and publish to nuget automatically
                    // add that package as a dependency
                    // and finally copy logic from micro.auth
                    throw new NotImplementedException();
                }
            };
        }
    }
}
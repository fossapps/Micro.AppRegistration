using System;
using System.Security.Claims;
using System.Text;
using Micro.AppRegistration.Business.Verification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Micro.AppRegistration.Api.Internal.UserDataExtensions
{
    public static class UserIdExtension
    {
        public static string? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static VerifySecretRequest GetAppCredentials(this IHttpContextAccessor httpContextAccessor)
        {
            StringValues auth = "";
            var exists = httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("authorization", out auth);
            if (exists == false)
            {
                return null;
            }

            var token = auth.ToString().Substring("Basic ".Length).Trim();
            var parts = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(":");
            if (parts.Length != 2)
            {
                return null;
            }

            return new VerifySecretRequest
            {
                AppId = parts[0],
                Secret = parts[1]
            };
        }
    }
}

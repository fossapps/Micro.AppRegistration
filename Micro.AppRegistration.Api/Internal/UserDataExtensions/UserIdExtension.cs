using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Micro.AppRegistration.Api.Internal.UserDataExtensions
{
    public static class UserIdExtension
    {
        public static string? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}

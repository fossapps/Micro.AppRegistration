using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Micro.AppRegistration.Api.UserDataExtensions
{
    public static class UserIdExtension
    {
        public static string? GetUserId(this ControllerBase controller)
        {
            return controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}

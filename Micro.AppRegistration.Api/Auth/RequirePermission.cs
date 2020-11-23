using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Micro.AppRegistration.Api.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequirePermission : Attribute, IAuthorizeData, IAuthorizationFilter
    {
        public string AuthenticationSchemes { get; set; }
        public string Policy { get; set; }
        public string Roles { get; set; }
        private readonly string _permission;

        public RequirePermission(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionClaims = context.HttpContext?.User?.Claims?.Where(c => c.Type == CustomClaims.Permission);
            var hasClaim = permissionClaims?.Any(x => x.Value == _permission || x.Value == "sudo");
            if (!hasClaim.HasValue || !hasClaim.Value)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

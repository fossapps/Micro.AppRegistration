using System;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.UserDataExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.AppRegistration.Api.CreateApplication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppRegistrationController : ControllerBase
    {
        private readonly IAppRegistrationService _appRegistrationService;

        public AppRegistrationController(IAppRegistrationService appRegistrationService)
        {
            _appRegistrationService = appRegistrationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateApplication(CreateApplicationRequest request)
        {
            try
            {
                var application = await _appRegistrationService.CreateApplication(request, this.GetUserId());
                return Ok(application);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "internal server error"
                });
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.UserDataExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.AppRegistration.Api.ListApplications
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IListApplicationService _listApplication;

        public ApplicationsController(IListApplicationService listApplication)
        {
            _listApplication = listApplication;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListApplications()
        {
            try
            {
                return Ok(await _listApplication.FindByOwner(this.GetUserId()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "internal server error"
                });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(string id)
        {
            try
            {
                return Ok(await _listApplication.FindByAppId(id, this.GetUserId()));
            }
            catch (UnauthorizedRequestException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ProblemDetails
                {
                    Title = "you don't have access to this application"
                });
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
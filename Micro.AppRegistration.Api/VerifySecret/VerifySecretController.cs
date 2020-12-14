using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.AppRegistration.Api.VerifySecret
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerifySecretController : ControllerBase
    {
        private readonly IVerifySecretService _verifySecretService;

        public VerifySecretController(IVerifySecretService verifySecretService)
        {
            _verifySecretService = verifySecretService;
        }

        [HttpPost]
        public async Task<IActionResult> Verify([FromHeader(Name = "Authorization")] [Required] [StartsWith("Basic ")]
            string authorization)
        {
            try
            {
                var (appId, secret) = GetBasicAuthData(authorization);
                var result = await _verifySecretService.Verify(appId, secret);
                return Ok(new VerifySecretResponse
                {
                    Success = result
                });
            }
            catch (BadBasicAuthorizationDataException e)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "authorization data is invalid"
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "error handling request"
                });
            }
        }

        private static (string, string) GetBasicAuthData(string authorizationHeader)
        {
            try
            {
                var token = authorizationHeader.Substring("Basic ".Length).Trim();
                var parts = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(":");
                return (parts[0], parts[1]);
            }
            catch (Exception e)
            {
                throw new BadBasicAuthorizationDataException("bad data", e);
            }
        }
    }
}

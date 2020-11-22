using System.Threading.Tasks;
using Micro.AppRegistration.Api.Models;

namespace Micro.AppRegistration.Api.CreateApplication
{
    public interface IAppRegistrationService
    {
        public Task<Application> CreateApplication(CreateApplicationRequest request, string userId);
    }

    public class AppRegistrationService : IAppRegistrationService
    {
        public async Task<Application> CreateApplication(CreateApplicationRequest request, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

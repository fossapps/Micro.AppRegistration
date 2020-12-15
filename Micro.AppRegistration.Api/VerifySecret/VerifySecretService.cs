using System.Threading.Tasks;
using Micro.AppRegistration.Api.ListApplications;
using Micro.AppRegistration.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Micro.AppRegistration.Api.VerifySecret
{
    public interface IVerifySecretService
    {
        Task<VerifySecretResponse> Verify(string appId, string secret);
    }
    public class VerifySecretService : IVerifySecretService
    {
        private readonly IPasswordHasher<Application> _secretHasher;
        private readonly IListApplicationRepository _applicationRepository;

        public VerifySecretService(IPasswordHasher<Application> secretHasher, IListApplicationRepository applicationRepository)
        {
            _secretHasher = secretHasher;
            _applicationRepository = applicationRepository;
        }

        public async Task<VerifySecretResponse> Verify(string appId, string secret)
        {
            var application = await _applicationRepository.FindById(appId);
            var result = _secretHasher.VerifyHashedPassword(null, application.Secret, secret);
            // todo: if result returns a rehash needed, we need re-hash password and save it in database.
            // to be done after MVP
            var success = result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
            return new VerifySecretResponse
            {
                Success = success,
                Approved = application.Approved,
                Owner = application.User,
                ShortCode = application.ShortCode,
                UseDefaultShortCode = application.UseDefaultCode
            };
        }
    }
}

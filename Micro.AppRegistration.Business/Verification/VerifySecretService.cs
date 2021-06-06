using System.Threading.Tasks;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Identity;

namespace Micro.AppRegistration.Business.Verification
{
    public interface IVerifySecretService
    {
        Task<Application> Verify(VerifySecretRequest credentials);
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

        public async Task<Application> Verify(VerifySecretRequest credentials)
        {
            if (credentials == null)
            {
                throw new NoBasicAuthException();
            }
            var application = await _applicationRepository.FindById(credentials.AppId);
            var result = _secretHasher.VerifyHashedPassword(null, application.Secret, credentials.Secret);
            // todo: if result returns a rehash needed, we need re-hash password and save it in database.
            // to be done after MVP
            var success = result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
            if (!success)
            {
                throw new CredentialsMismatchException();
            }

            application.Secret = null;
            return application;
        }
    }
}

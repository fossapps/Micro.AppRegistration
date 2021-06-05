using System;
using System.Threading.Tasks;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Identity;

namespace Micro.AppRegistration.Business.Verification
{
    public interface IVerifySecretService
    {
        Task<Application> Verify(string appId, string secret);
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

        public async Task<Application> Verify(string appId, string secret)
        {
            // var application = await _applicationRepository.FindById(appId);
            // var result = _secretHasher.VerifyHashedPassword(null, application.Secret, secret);
            // todo: if result returns a rehash needed, we need re-hash password and save it in database.
            // to be done after MVP
            // var success = result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
            // if (!success)
            // {
                // throw new CredentialsMismatchException();
            // }
            // return application;
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.Models;
using Micro.AppRegistration.Api.Uuid;
using Microsoft.AspNetCore.Identity;

namespace Micro.AppRegistration.Api.CreateApplication
{
    public interface IAppRegistrationService
    {
        public Task<Application> CreateApplication(CreateApplicationRequest request, string userId);
    }

    public class AppRegistrationService : IAppRegistrationService
    {
        private readonly IPasswordHasher<Application> _hasher;
        private readonly ICreateApplicationRepository _applicationRepository;

        public AppRegistrationService(IPasswordHasher<Application> hasher, ICreateApplicationRepository applicationRepository)
        {
            _hasher = hasher;
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> CreateApplication(CreateApplicationRequest request, string userId)
        {
            var secret = GenerateApplicationSecret();
            var hashedSecret = _hasher.HashPassword(null, secret);
            var application = await _applicationRepository.Create(new Application
            {
                Approved = false,
                Name = request.Name,
                Secret = hashedSecret,
                User = userId,
                CreatedBy = userId,
                UseDefaultCode = request.UseDefault,
                ShortCode = request.ShortCode
            });
            application.Secret = secret;
            return application;
        }

        private static string GenerateApplicationSecret()
        {
            return Convert.ToBase64String(GenerateRandomBytes(256));
        }

        private static byte[] GenerateRandomBytes(int keyBitLength)
        {
            using var provider = new RNGCryptoServiceProvider();
            var lengthInByte = keyBitLength / 8;
            var randomNumber = new byte[lengthInByte];
            provider.GetBytes(randomNumber);
            return randomNumber;
        }
    }
}

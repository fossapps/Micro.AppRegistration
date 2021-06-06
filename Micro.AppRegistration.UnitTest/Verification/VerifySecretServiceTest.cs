using System;
using System.Threading.Tasks;
using Micro.AppRegistration.Business.Verification;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace Micro.AppRegistration.UnitTest.Verification
{
    public class VerifySecretServiceTest
    {
        [Test]
        public void TestVerifyThrowsExceptionIfNoCredentialsProvided()
        {
            var secretService = new VerifySecretService(null, null);
            Assert.ThrowsAsync<NoBasicAuthException>(() => secretService.Verify(null));
        }

        [Test]
        public void TestVerifyThrowsExceptionIfCredentialsMismatched()
        {
            var mockPasswordHasher = new Mock<IPasswordHasher<Application>>();
            mockPasswordHasher
                .Setup(x => x.VerifyHashedPassword(It.IsAny<Application>(), "some-secret", "providedPassword"))
                .Returns(PasswordVerificationResult.Failed);
            var mockRepo = new Mock<IListApplicationRepository>();
            mockRepo.Setup(x => x.FindById("test-id")).ReturnsAsync(new Application
            {
                Id = "test-id",
                Name = "test-name",
                Secret = "some-secret",
                User = "test-user",
                CreatedAt = DateTime.Now
            });
            var service = new VerifySecretService(mockPasswordHasher.Object, mockRepo.Object);
            var request = new VerifySecretRequest
            {
                Secret = "providedPassword",
                AppId = "test-id"
            };
            Assert.ThrowsAsync<CredentialsMismatchException>(() => service.Verify(request));
        }

        [Test]
        public async Task TestVerifyReturnsApplicationIfCredentialsMatches()
        {
            var mockPasswordHasher = new Mock<IPasswordHasher<Application>>();
            mockPasswordHasher
                .Setup(x => x.VerifyHashedPassword(It.IsAny<Application>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            var mockRepo = new Mock<IListApplicationRepository>();
            var mockApplication = new Application
            {
                Id = "test-id",
                Name = "test-name",
                Secret = "some-secret",
                User = "test-user",
                CreatedAt = DateTime.Now
            };
            mockRepo.Setup(x => x.FindById("test-id")).ReturnsAsync(mockApplication);

            var service = new VerifySecretService(mockPasswordHasher.Object, mockRepo.Object);
            var request = new VerifySecretRequest
            {
                Secret = "providedPassword",
                AppId = "test-id"
            };
            var response = await service.Verify(request);
            Assert.AreSame(mockApplication, response);
        }

        [Test]
        public async Task TestVerifyRemovesSecretInApplicationResult()
        {
            var mockPasswordHasher = new Mock<IPasswordHasher<Application>>();
            mockPasswordHasher
                .Setup(x => x.VerifyHashedPassword(It.IsAny<Application>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            var mockRepo = new Mock<IListApplicationRepository>();
            var mockApplication = new Application
            {
                Id = "test-id",
                Name = "test-name",
                Secret = "some-secret",
                User = "test-user",
                CreatedAt = DateTime.Now
            };
            mockRepo.Setup(x => x.FindById("test-id")).ReturnsAsync(mockApplication);

            var service = new VerifySecretService(mockPasswordHasher.Object, mockRepo.Object);
            var request = new VerifySecretRequest
            {
                Secret = "providedPassword",
                AppId = "test-id"
            };
            var response = await service.Verify(request);
            Assert.Null(response.Secret);
        }
    }
}

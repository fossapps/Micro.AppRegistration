using System;
using System.Linq;
using System.Threading.Tasks;
using Micro.AppRegistration.Business.CreateApplication;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace Micro.AppRegistration.UnitTest.CreateApplication
{
    public class AppRegistrationServiceTest
    {
        [Test]
        public async Task CreateApplicationSavesToRepoAndUsesOriginalSecret()
        {
            var mockHasher = new Mock<IPasswordHasher<Application>>();
            mockHasher.Setup(x => x.HashPassword(It.IsAny<Application>(), It.IsAny<string>()))
                .Returns("hashed-secret");

            var mockRepo = new Mock<ICreateApplicationRepository>();
            mockRepo.Setup(x => x.Create(It.Is<Application>(a => a.Secret == "hashed-secret" && a.Name == "name" && a.User == "user-id")))
                .ReturnsAsync(new Application
                {
                    Id = "id",
                    Name = "name",
                    Secret = "hashed-secret",
                    User = "user-id",
                    CreatedAt = DateTime.Now
                });
            var service = new AppRegistrationService(mockHasher.Object, mockRepo.Object);
            var application = await service.CreateApplication(new CreateApplicationRequest {Name = "name"}, "user-id");
            Assert.AreNotEqual("hashed-secret", application.Secret);
            var argument = (string) mockHasher.Invocations.FirstOrDefault()?.Arguments[1];
            Assert.AreEqual(argument, application.Secret);
        }
    }
}

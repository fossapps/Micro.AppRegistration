using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.AppRegistration.Business.ListApplication;
using Micro.AppRegistration.Storage;
using Moq;
using NUnit.Framework;

namespace Micro.AppRegistration.UnitTest.ListApplication
{
    public class ListApplicationTest
    {
        [Test]
        public async Task FindByIds_RemovesSecret()
        {
            var mockRepo = new Mock<IListApplicationRepository>();
            var mockAppList = new List<Application>
            {
                new Application
                {
                    Id = "1",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user",
                    CreatedAt = DateTime.Now
                },
                new Application
                {
                    Id = "2",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user",
                    CreatedAt = DateTime.Now
                },
                new Application
                {
                    Id = "3",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user",
                    CreatedAt = DateTime.Now
                },
            };
            mockRepo.Setup(x => x.FindByIds(It.IsAny<IEnumerable<string>>())).ReturnsAsync(mockAppList);
            var service = new ListApplicationsService(mockRepo.Object);
            var apps = await service.FindByIds(new[] {"1", "2", "3"});
            foreach (var application in apps.ToList())
            {
                Assert.Null(application.Value.Secret);
            }
        }

        [Test]
        public async Task FindByOwnerIdsConvertsToLookupAndRemovesSecret()
        {
            var mockRepo = new Mock<IListApplicationRepository>();
            var mockAppList = new List<Application>
            {
                new Application
                {
                    Id = "1",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user",
                    CreatedAt = DateTime.Now
                },
                new Application
                {
                    Id = "2",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user",
                    CreatedAt = DateTime.Now
                },
                new Application
                {
                    Id = "3",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user1",
                    CreatedAt = DateTime.Now
                },
                new Application
                {
                    Id = "3",
                    Name = "test-name",
                    Secret = "secret",
                    User = "user2",
                    CreatedAt = DateTime.Now
                },
            };
            mockRepo.Setup(x => x.ListApplicationsByOwners(It.IsAny<IEnumerable<string>>())).ReturnsAsync(mockAppList);
            var service = new ListApplicationsService(mockRepo.Object);
            var apps = await service.FindByOwnerIds(new[] {"user", "user1"});
            foreach (var application in apps["user"].ToList())
            {
                Assert.Null(application.Secret);
                Assert.AreEqual("user", application.User);
            }
            foreach (var application in apps["user1"].ToList())
            {
                Assert.Null(application.Secret);
                Assert.AreEqual("user1", application.User);
            }
        }

        [Test]
        public void VerifyOwnership_ThrowsExceptionIfOwnerDoesNotMatch()
        {
            IListApplicationService service = new ListApplicationsService(null);
            Assert.ThrowsAsync<UnauthorizedException>(() => service.VerifyOwnership(new Application { User = "user-id-2" }, "user-id"));
        }

        [Test]
        public async Task VerifyOwnership_ReturnsOriginalApplicationIfUserMatches()
        {
            IListApplicationService service = new ListApplicationsService(null);
            var mockApp = new Application
            {
                User = "user-id"
            };
            var app = await service.VerifyOwnership(mockApp, "user-id");
            Assert.AreSame(mockApp, app);
        }
    }
}

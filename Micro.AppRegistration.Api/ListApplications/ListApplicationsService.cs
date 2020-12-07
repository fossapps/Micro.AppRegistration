using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.Models;

namespace Micro.AppRegistration.Api.ListApplications
{
    public interface IListApplicationService
    {
        public Task<IEnumerable<ApplicationResponse>> FindByOwner(string ownerId);
        public Task<ApplicationResponse> FindByAppId(string appId, string userId);
    }
    public class ListApplicationsService : IListApplicationService
    {
        private readonly IListApplicationRepository _repository;

        private static ApplicationResponse MapApplicationResponse(Application application)
        {
            return new ApplicationResponse
            {
                Approved = application.Approved,
                Id = application.Id,
                Name = application.Name,
                User = application.User,
                CreatedAt = application.CreatedAt,
                CreatedBy = application.CreatedBy,
                ShortCode = application.ShortCode,
                UseDefaultCode = application.UseDefaultCode
            };
        }
        public ListApplicationsService(IListApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ApplicationResponse>> FindByOwner(string ownerId)
        {
            var applications = await _repository.ListApplicationsByOwner(ownerId);
            return applications.Select(MapApplicationResponse);
        }

        public async Task<ApplicationResponse> FindByAppId(string appId, string ownerId)
        {
            var app = await _repository.FindById(appId);
            if (ownerId != app.User)
            {
                throw new UnauthorizedRequestException();
            }
            return MapApplicationResponse(app);
        }
    }
}

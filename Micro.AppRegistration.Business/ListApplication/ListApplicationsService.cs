using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.AppRegistration.Storage;

namespace Micro.AppRegistration.Business.ListApplication
{
    public interface IListApplicationService
    {
        public Task<IEnumerable<Application>> FindByOwner(string ownerId);
    }
    public class ListApplicationsService : IListApplicationService
    {
        private readonly IListApplicationRepository _repository;

        public ListApplicationsService(IListApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Application>> FindByOwner(string ownerId)
        {
            return await _repository.ListApplicationsByOwner(ownerId);
        }
    }
}

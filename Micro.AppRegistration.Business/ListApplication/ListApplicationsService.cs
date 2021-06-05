using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.AppRegistration.Storage;

namespace Micro.AppRegistration.Business.ListApplication
{
    public interface IListApplicationService
    {
        Task<Dictionary<string, Application>> FindByIds(IEnumerable<string> ids);
        Task<ILookup<string, Application>> FindByOwnerIds(IEnumerable<string> ids);
    }
    public class ListApplicationsService : IListApplicationService
    {
        private readonly IListApplicationRepository _repository;

        public ListApplicationsService(IListApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<string, Application>> FindByIds(IEnumerable<string> ids)
        {
            return (await _repository.FindByIds(ids)).Select(RemoveSecret).ToDictionary(x => x.Id);
        }

        public async Task<ILookup<string, Application>> FindByOwnerIds(IEnumerable<string> ids)
        {
            return (await _repository.ListApplicationsByOwners(ids)).Select(RemoveSecret).ToLookup(x => x.User);
        }

        private static Application RemoveSecret(Application app)
        {
            app.Secret = null;
            return app;
        }
    }
}

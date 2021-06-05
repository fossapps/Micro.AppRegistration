using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using Micro.AppRegistration.Business.ListApplication;
using Micro.AppRegistration.Storage;

namespace Micro.AppRegistration.Api.GraphQL.DataLoaders
{
    public class ApplicationByOwnerLoader : DataLoaderBase<string, IEnumerable<Application>>
    {
        private readonly IListApplicationService _applicationService;

        public ApplicationByOwnerLoader(IListApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, IEnumerable<Application>>> list, CancellationToken cancellationToken)
        {
            var users = list.Select(x => x.Key);
            var applications = await _applicationService.FindByOwnerIds(users);
            foreach (var entry in list)
            {
                entry.SetResult(applications[entry.Key]);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using Micro.AppRegistration.Business.ListApplication;
using Micro.AppRegistration.Storage;

namespace Micro.AppRegistration.Api.GraphQL.DataLoaders
{
    public class ApplicationByIdLoader : DataLoaderBase<string, Application>
    {
        private readonly IListApplicationService _applicationService;

        public ApplicationByIdLoader(IListApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, Application>> list, CancellationToken cancellationToken)
        {
            var ids = list.Select(x => x.Key).ToList();
            var applications = await _applicationService.FindByIds(ids);
            foreach (var entry in list)
            {
                var exists = applications.TryGetValue(entry.Key, out var application);
                entry.SetResult(exists ? application : null);
            }
        }
    }
}

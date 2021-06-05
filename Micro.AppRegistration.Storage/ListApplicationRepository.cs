using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Micro.AppRegistration.Storage
{
    public interface IListApplicationRepository
    {
        public Task<IEnumerable<Application>> ListApplicationsByOwners(IEnumerable<string> ownerIds);
        public Task<IEnumerable<Application>> FindByIds(IEnumerable<string> ids);
    }

    public class ListApplicationRepository : IListApplicationRepository
    {
        private readonly ApplicationContext _db;

        public ListApplicationRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Application>> ListApplicationsByOwners(IEnumerable<string> ownerIds)
        {
            return await _db.Applications.AsNoTracking().Where(x => ownerIds.Contains(x.User)).ToListAsync();
        }

        public async Task<IEnumerable<Application>> FindByIds(IEnumerable<string> ids)
        {
            return await _db.Applications.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}

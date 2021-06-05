using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Micro.AppRegistration.Storage
{
    public interface IListApplicationRepository
    {
        public Task<IEnumerable<Application>> ListApplicationsByOwner(string ownerId);
        public Task<Application> FindById(string id);
    }

    public class ListApplicationRepository : IListApplicationRepository
    {
        private readonly ApplicationContext _db;

        public ListApplicationRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Application>> ListApplicationsByOwner(string ownerId)
        {
            return await _db.Applications.AsNoTracking().Where(x => x.User == ownerId).ToListAsync();
        }

        public async Task<Application> FindById(string id)
        {
            return await _db.Applications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

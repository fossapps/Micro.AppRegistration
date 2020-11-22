using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.Models;

namespace Micro.AppRegistration.Api.Repository
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<Weather>> GetAll();
        Task<Weather> FindById(string id);
        Task<Weather> Create([NotNull] Weather weather);
        Task Delete(string id);
    }
}

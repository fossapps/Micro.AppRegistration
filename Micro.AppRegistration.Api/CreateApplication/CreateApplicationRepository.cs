using System;
using System.Threading.Tasks;
using Micro.AppRegistration.Api.Models;
using Micro.AppRegistration.Api.Uuid;

namespace Micro.AppRegistration.Api.CreateApplication
{
    public interface ICreateApplicationRepository
    {
        public Task<Application> Create(Application application);
    }
    public class CreateApplicationRepository : ICreateApplicationRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IUuidService _uuidService;

        public CreateApplicationRepository(ApplicationContext applicationContext, IUuidService uuidService)
        {
            _applicationContext = applicationContext;
            _uuidService = uuidService;
        }

        public async Task<Application> Create(Application application)
        {
            var result = await _applicationContext.Applications.AddAsync(new Application
            {
                Id = _uuidService.GenerateUuId(),
                User = application.User,
                Approved = application.Approved,
                Name = application.Name,
                Secret = application.Secret,
                CreatedAt = DateTime.Now,
                CreatedBy = application.CreatedBy,
                ShortCode = application.ShortCode,
                UseDefaultCode = application.UseDefaultCode
            });
            await _applicationContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
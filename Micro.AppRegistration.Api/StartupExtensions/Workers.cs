using Micro.AppRegistration.Api.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.StartupExtensions
{
    public static class Workers
    {
        public static void RegisterWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }
    }
}

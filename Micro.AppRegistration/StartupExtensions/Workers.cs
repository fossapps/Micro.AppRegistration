using Micro.AppRegistration.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.StartupExtensions
{
    public static class Workers
    {
        public static void RegisterWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }
    }
}

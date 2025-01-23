using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Roots;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class RootsServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActivitiesRoot>();
            services.AddSingleton<DataRoot>();
            services.AddSingleton<TrainingsRoot>();
            services.AddSingleton<ActivitiesDataRoot>();
            services.AddSingleton<MachinesRoot>();
        }
    }
}
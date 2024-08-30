using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.God;
using Shopfloor.Features.WorkInProgressFeature;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<WorkInProgressViewModel>();
            services.AddSingleton<GodViewModel>();
        }
    }
}
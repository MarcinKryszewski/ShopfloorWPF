using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Roots;

namespace Shopfloor.Hosts.Features
{
    internal static class RootsServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActivitiesRoot>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Contexts;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class ContextServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActivityContext>();
            services.AddSingleton<MachineContext>();
        }
    }
}
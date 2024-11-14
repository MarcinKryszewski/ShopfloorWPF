using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Contexts;

namespace Shopfloor.Hosts.Features
{
    internal static class ContextServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActivityContext>();
        }
    }
}
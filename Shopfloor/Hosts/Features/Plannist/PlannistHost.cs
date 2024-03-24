using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard;

namespace Shopfloor.Hosts.Features.Plannist
{
    internal sealed class PlannistHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<PlannistDashboardViewModel>();
        }
    }
}
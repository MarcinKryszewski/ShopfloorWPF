using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.ManagerDashboard;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Manager.Stores;

namespace Shopfloor.Hosts.Features.Manager
{
    internal sealed class ManagerHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ManagerDashboardViewModel>();
            services.AddSingleton<OrderApproveViewModel>();
            services.AddSingleton<OrdersToApproveViewModel>();

            services.AddSingleton<SelectedRequestStore>();
        }
    }
}
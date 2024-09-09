using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.God;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Hosts.Features.Notifier;
using Shopfloor.Models.WorkOrders;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<WorkInProgressViewModel>();
            services.AddSingleton<GodViewModel>();
            NotifierServices.Get(services);

            services.AddSingleton<WorkOrderRepository>();
            services.AddSingleton<WorkOrderStore>();
            services.AddSingleton<WorkOrdersListRoot>();
            services.AddTransient<WorkOrdersListViewModel>();
        }
    }
}
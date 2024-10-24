using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.WorkOrderAddNew;
using Shopfloor.Features.Mechanic.WorkOrderDetails;
using Shopfloor.Features.Mechanic.WorkOrderEdit;
using Shopfloor.Features.Mechanic.WorkOrdersList;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.Plannist.OrdersList;
using Shopfloor.Roots;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesServices
    {
        public static void Get(IServiceCollection services)
        {
            Mechanic(services);
            Plannist(services);
            Parts(services);
        }
        private static void Plannist(IServiceCollection services)
        {
            services.AddSingleton<OrdersListRoot>();
            services.AddTransient<OrdersListViewModel>();
        }
        private static void Parts(IServiceCollection services)
        {
            services.AddSingleton<PartsListRoot>();
            services.AddTransient<PartsListViewModel>();
        }
        private static void Mechanic(IServiceCollection services)
        {
            services.AddSingleton<WorkOrdersListRoot>();
            services.AddTransient<WorkOrdersListViewModel>();

            services.AddSingleton<WorkOrderCreateRoot>();
            services.AddTransient<WorkOrderAddNewViewModel>();

            services.AddSingleton<WorkOrderEditRoot>();
            services.AddTransient<WorkOrderEditViewModel>();

            services.AddTransient<WorkOrderDetailsRoot>();
            services.AddTransient<WorkOrderDetailsViewModel>();
        }
    }
}
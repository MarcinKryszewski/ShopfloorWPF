using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.God;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Features.WorkOrderAddNew;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Hosts.Features.Notifier;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
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

            services.AddSingleton<IRepository<WorkOrderModel, WorkOrderCreationModel>, WorkOrderRepository>();
            services.AddSingleton<IStore<WorkOrderModel>, WorkOrderStore>();

            services.AddSingleton<IRepository<LineModel, LineCreationModel>, LineRepository>();
            services.AddSingleton<IStore<LineModel>, LineStore>();

            services.AddSingleton<WorkOrdersListRoot>();
            services.AddTransient<WorkOrdersListViewModel>();

            services.AddSingleton<WorkOrderCreateRoot>();
            services.AddSingleton<WorkOrderAddNewViewModel>();
        }
    }
}
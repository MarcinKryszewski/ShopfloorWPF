using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Contexts;
using Shopfloor.Features.God;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Features.WorkOrderAddNew;
using Shopfloor.Features.WorkOrderDetails;
using Shopfloor.Features.WorkOrderEdit;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Hosts.Features.Notifier;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<WorkInProgressViewModel>();
            services.AddSingleton<GodViewModel>();

            NotifierServices.Get(services);

            Contexts(services);

            WorkOrderServices(services);
            LinesServices(services);

            Features(services);
        }

        private static void Contexts(IServiceCollection services)
        {
            services.AddSingleton<WorkOrderContext>();
        }
        private static void Features(IServiceCollection services)
        {
            WorkOrders(services);
            Parts(services);
        }

        private static void Parts(IServiceCollection services)
        {
            services.AddTransient<PartsListViewModel>();
        }

        private static void WorkOrders(IServiceCollection services)
        {
            services.AddSingleton<WorkOrdersListRoot>();
            services.AddTransient<WorkOrdersListViewModel>();

            services.AddSingleton<WorkOrderCreateRoot>();
            services.AddTransient<WorkOrderAddNewViewModel>();

            services.AddSingleton<WorkOrderEditRoot>();
            services.AddTransient<WorkOrderEditViewModel>();

            services.AddTransient<WorkOrderDetailsViewModel>();
        }
        private static void LinesServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<LineModel, LineCreationModel>, LineRepository>();
            services.AddSingleton<IStore<LineModel>, LineStore>();
        }

        private static void WorkOrderServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<WorkOrderModel, WorkOrderCreationModel>, WorkOrderRepository>();
            services.AddSingleton<IStore<WorkOrderModel>, WorkOrderStore>();
        }
    }
}
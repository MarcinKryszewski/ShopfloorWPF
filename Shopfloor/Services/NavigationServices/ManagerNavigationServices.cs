using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class ManagerNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            AddStoresToServices(services);
            GetOrdersToApproveNavigation(services, databaseServices);
            GetOrderApproveNavigation(services, databaseServices);
        }
        public static void AddStoresToServices(IServiceCollection services)
        {
            services.AddSingleton<SelectedRequestStore>();
        }
        private static void GetOrdersToApproveNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateOrdersToApproveViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<OrdersToApproveViewModel>>((s) => () => s.GetRequiredService<OrdersToApproveViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<OrdersToApproveViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<OrdersToApproveViewModel>>()
                );
            });
        }
        private static void GetOrderApproveNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateOrderApproveViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<OrderApproveViewModel>>((s) => () => s.GetRequiredService<OrderApproveViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<OrderApproveViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<OrderApproveViewModel>>()
                );
            });
        }
        private static OrdersToApproveViewModel CreateOrdersToApproveViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static OrderApproveViewModel CreateOrderApproveViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
    }
}
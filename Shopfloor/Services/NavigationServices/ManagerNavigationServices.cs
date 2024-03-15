using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.ManagerDashboard;
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
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            AddStoresToServices(services);
            GetDashboardNavigation(services);
            GetOrdersToApproveNavigation(services, databaseServices);
            GetOrderApproveNavigation(services, databaseServices, userServices);
        }
        public static void AddStoresToServices(IServiceCollection services)
        {
            services.AddSingleton<SelectedRequestStore>();
        }
        private static void GetDashboardNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateDashboardViewModel(s));
            services.AddSingleton<CreateViewModel<ManagerDashboardViewModel>>((s) => () => s.GetRequiredService<ManagerDashboardViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ManagerDashboardViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ManagerDashboardViewModel>>()
                );
            });
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
        private static void GetOrderApproveNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateOrderApproveViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<OrderApproveViewModel>>((s) => () => s.GetRequiredService<OrderApproveViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<OrderApproveViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<OrderApproveViewModel>>()
                );
            });
        }
        private static ManagerDashboardViewModel CreateDashboardViewModel(IServiceProvider services) => new();
        private static OrdersToApproveViewModel CreateOrdersToApproveViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static OrderApproveViewModel CreateOrderApproveViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new(services, databaseServices, userServices);
    }
}
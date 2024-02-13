using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.ControlPanel;
using Shopfloor.Features.Plannist.Deploys;
using Shopfloor.Features.Plannist.Orders;
using Shopfloor.Features.Plannist.Reports;
using Shopfloor.Features.Plannist.Reservations;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class PlannistNavigationServices
    {
        internal static void Get(IServiceCollection services)
        {
            GetControlPanelNavigation(services);
            GetDeploysNavigation(services);
            GetOrdersNavigation(services);
            GetReportsNavigation(services);
            GetReservationsNavigation(services);
        }

        private static void GetControlPanelNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateControlPanelViewModel(s));
            services.AddSingleton<CreateViewModel<ControlPanelViewModel>>((s) => () => s.GetRequiredService<ControlPanelViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ControlPanelViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ControlPanelViewModel>>()
                );
            });
        }

        private static void GetDeploysNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateDeploysViewModel(s));
            services.AddSingleton<CreateViewModel<DeploysViewModel>>((s) => () => s.GetRequiredService<DeploysViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<DeploysViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<DeploysViewModel>>()
                );
            });
        }

        private static void GetOrdersNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateOrdersViewModel(s));
            services.AddSingleton<CreateViewModel<OrdersViewModel>>((s) => () => s.GetRequiredService<OrdersViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<OrdersViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<OrdersViewModel>>()
                );
            });
        }

        private static void GetReportsNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateReportsViewModel(s));
            services.AddSingleton<CreateViewModel<ReportsViewModel>>((s) => () => s.GetRequiredService<ReportsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ReportsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ReportsViewModel>>()
                );
            });
        }

        private static void GetReservationsNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateReservationsViewModel(s));
            services.AddSingleton<CreateViewModel<ReservationsViewModel>>((s) => () => s.GetRequiredService<ReservationsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ReservationsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ReservationsViewModel>>()
                );
            });
        }

        private static ControlPanelViewModel CreateControlPanelViewModel(IServiceProvider services)
        {
            return new ControlPanelViewModel(services);
        }

        private static DeploysViewModel CreateDeploysViewModel(IServiceProvider services)
        {
            return new DeploysViewModel(services);
        }

        private static OrdersViewModel CreateOrdersViewModel(IServiceProvider services)
        {
            return new OrdersViewModel(services);
        }

        private static ReportsViewModel CreateReportsViewModel(IServiceProvider services)
        {
            return new ReportsViewModel();
        }

        private static ReservationsViewModel CreateReservationsViewModel(IServiceProvider services)
        {
            return new ReservationsViewModel();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.ControlPanel;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Deploys;
using Shopfloor.Features.Orders;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Hosts.MainHost
{
    public class MainHost
    {
        public static IHost GetHost()
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<SidePanelViewModel>();
                services.AddSingleton<ContentViewModel>();
                services.AddTransient<TopPanelViewModel>();
                services.AddSingleton<NavigationStore>();

                #region Dashboard
                services.AddTransient((s) => CreateDashboardViewModel(s));
                services.AddSingleton<CreateViewModel<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<DashboardViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<DashboardViewModel>>()
                    );
                });
                #endregion
                #region ControlPanel
                services.AddTransient((s) => CreateControlPanelViewModel(s));
                services.AddSingleton<CreateViewModel<ControlPanelViewModel>>((s) => () => s.GetRequiredService<ControlPanelViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<ControlPanelViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<ControlPanelViewModel>>()
                    );
                });
                #endregion
                #region Orders
                services.AddTransient((s) => CreateOrdersViewModel(s));
                services.AddSingleton<CreateViewModel<OrdersViewModel>>((s) => () => s.GetRequiredService<OrdersViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<OrdersViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<OrdersViewModel>>()
                    );
                });
                #endregion
                #region Deploys
                services.AddTransient((s) => CreateDeploysViewModel(s));
                services.AddSingleton<CreateViewModel<DeploysViewModel>>((s) => () => s.GetRequiredService<DeploysViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<DeploysViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<DeploysViewModel>>()
                    );
                });
                #endregion

            })
            .Build();
        }

        private static DashboardViewModel CreateDashboardViewModel(IServiceProvider services)
        {
            return new DashboardViewModel(services);
        }
        private static ControlPanelViewModel CreateControlPanelViewModel(IServiceProvider services)
        {
            return new ControlPanelViewModel(services);
        }
        private static OrdersViewModel CreateOrdersViewModel(IServiceProvider services)
        {
            return new OrdersViewModel(services);
        }
        private static DeploysViewModel CreateDeploysViewModel(IServiceProvider services)
        {
            return new DeploysViewModel(services);
        }
    }
}
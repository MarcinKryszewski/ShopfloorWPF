using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.SomeFeature;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

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

                services.AddTransient((s) => CreateDashboardViewModel(s));
                services.AddSingleton<CreateViewModel<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<DashboardViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<DashboardViewModel>>()
                    );
                });

                services.AddTransient((s) => CreateSomeFeatureViewModel(s));
                services.AddSingleton<CreateViewModel<SomeFeatureViewModel>>((s) => () => s.GetRequiredService<SomeFeatureViewModel>());
                services.AddSingleton((s) =>
                {
                    return new NavigationService<SomeFeatureViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        s.GetRequiredService<CreateViewModel<SomeFeatureViewModel>>()
                    );
                });

            })
            .Build();
        }

        private static DashboardViewModel CreateDashboardViewModel(IServiceProvider services)
        {
            return new DashboardViewModel(services);
        }

        private static SomeFeatureViewModel CreateSomeFeatureViewModel(IServiceProvider services)
        {
            return new SomeFeatureViewModel(services);
        }
    }
}
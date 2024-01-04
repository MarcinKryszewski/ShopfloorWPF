using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Stores;

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

                DashboardNavigationServices.Get(services);
                MechanicNavigationServices.Get(services);
                PlannistNavigationServices.Get(services);
                ManagerNavigationServices.Get(services);
                AdminNavigationServices.Get(services);

            })
            .Build();
        }
    }
}
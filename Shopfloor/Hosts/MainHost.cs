using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Dashboard;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Hosts.MainHost
{
    public class MainHost
    {
        public static IHost GetHost()
        {

            NavigationStore navigationStore = new()
            {
                CurrentViewModel = new DashboardViewModel()
            };

            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<SidePanelViewModel>();
                services.AddSingleton<ContentViewModel>();
                services.AddTransient<TopPanelViewModel>();
                services.AddSingleton(navigationStore);
            })
            .Build();
        }
    }
}
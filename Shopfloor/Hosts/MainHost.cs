using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Stores;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Hosts.MainHost
{
    internal sealed class MainHost
    {
        public static IHost GetHost(IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(userServices.GetRequiredService<CurrentUserStore>());

                services.AddSingleton<SidePanelViewModel>(provider =>
                {
                    //DELETE AFTER DEVELOPEMENT -> FOR BUTTON ADDING TEST DATA
                    return new SidePanelViewModel(provider, databaseServices);
                });
                services.AddSingleton<ContentViewModel>();
                services.AddTransient<TopPanelViewModel>();
                services.AddSingleton<NavigationStore>();

                NotifierServices.Get(services);

                DashboardNavigationServices.Get(services);
                LoginNavigationServices.Get(services, databaseServices, userServices);
                MechanicNavigationServices.Get(services, databaseServices, userServices);
                PlannistNavigationServices.Get(services, databaseServices, userServices);
                ManagerNavigationServices.Get(services, databaseServices);
                AdminNavigationServices.Get(services, databaseServices);


            })
            .Build();
        }
    }
}
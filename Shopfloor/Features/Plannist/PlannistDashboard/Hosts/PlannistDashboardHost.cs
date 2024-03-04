using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Plannist.PlannistDashboard.Services;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Hosts
{
    public class PlannistDashboardHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedRequestStore>();

                PlannistDashboardNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Machines.Services;
using Shopfloor.Features.Admin.Machines.Stores;
using Shopfloor.Shared.Stores;
using System;


namespace Shopfloor.Features.Admin.Machines.Hosts
{
    public class MachinesHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedMachineStore>();

                MachinesNavigationService.Get(services, databaseServices);
            })
            .Build();
        }
    }
}

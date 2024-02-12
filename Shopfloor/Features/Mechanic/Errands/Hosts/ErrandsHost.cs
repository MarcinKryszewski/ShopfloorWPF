using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Mechanic.Errands.Services;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Mechanic.Errands.Hosts
{
    public class ErrandsHost
    {
        public static IHost GetHost(IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedUserStore>();

                services.AddSingleton<SelectedErrandStore>();

                ErrandsNavigationServices.Get(services, databaseServices, userServices);
            })
            .Build();
        }
    }
}
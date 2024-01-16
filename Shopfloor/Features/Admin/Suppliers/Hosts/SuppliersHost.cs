using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Suppliers.Services;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Admin.Suppliers.Hosts
{
    public class SuppliersHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();

                SuppliersNavigationService.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
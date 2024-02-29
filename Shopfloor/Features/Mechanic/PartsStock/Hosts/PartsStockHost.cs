using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.PartsStock.Services;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Mechanic.PartsStock.Hosts
{
    public class PartsStockHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();

                PartsStockNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
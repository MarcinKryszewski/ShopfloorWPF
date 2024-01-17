using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Parts.Services;
using Shopfloor.Shared.Stores;
using System;


namespace Shopfloor.Features.Admin.Parts.Hosts
{
    public class PartsHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();

                PartsNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
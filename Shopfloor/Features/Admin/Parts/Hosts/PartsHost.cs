using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Parts.Services;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Shared.Stores;
using System;

namespace Shopfloor.Features.Admin.Parts.Hosts
{
    internal sealed class PartsHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedPartStore>();

                PartsNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
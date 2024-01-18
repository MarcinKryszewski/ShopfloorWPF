using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.PartTypes.Services;
using Shopfloor.Shared.Stores;
using System;

namespace Shopfloor.Features.Admin.PartTypes.Hosts
{
    public class PartTypesHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();

                PartTypesNavigationService.Get(services, databaseServices);
            })
            .Build();
        }
    }
}

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.Requests.Services;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Mechanic.Requests.Hosts
{
    internal sealed class RequestsHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();

                RequestsNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
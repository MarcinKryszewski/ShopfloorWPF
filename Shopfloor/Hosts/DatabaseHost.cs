using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;

namespace Shopfloor.Hosts.DatabaseHost
{
    public class DatabaseHost
    {
        public static IHost GetHost(IServiceProvider configurationServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(new DatabaseConnectionFactory(configurationServices));
                ProvidersServices(services);
            })
            .Build();
        }

        private static void ProvidersServices(IServiceCollection services)
        {
        }
    }

}
using Microsoft.Extensions.Hosting;
using Shopfloor.Hosts.Core;
using Shopfloor.Hosts.Database;
using Shopfloor.Hosts.Features;

namespace Shopfloor.Hosts
{
    internal static class AppHost
    {
        public static IHost Get()
        {
            ConfigurationHost configurationHost = new();

            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                configurationHost.Get(services);
                DatabaseHost.Get(services);
                FeaturesHost.Get(services);
                NavigationHost.Get(services);
            })
            .Build();
        }
    }
}
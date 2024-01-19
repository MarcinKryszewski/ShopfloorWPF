using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Services.Providers;
using Shopfloor.Stores.DatabaseDataStores;
using System;

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
            services.AddSingleton<PartTypeProvider>();
            services.AddSingleton<UserProvider>();
            services.AddSingleton<RoleProvider>();
            services.AddSingleton<RoleUserProvider>();
            services.AddSingleton<MachineProvider>();
            services.AddSingleton<SupplierProvider>();
            services.AddSingleton<PartProvider>();

            services.AddSingleton<PartTypesStore>();
            services.AddSingleton<PartsStore>();
            services.AddSingleton<SuppliersStore>();
            services.AddSingleton<MachineStore>();
        }
    }

}
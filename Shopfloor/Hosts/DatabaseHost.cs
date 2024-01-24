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
            services.AddSingleton<MachinePartProvider>();
            services.AddSingleton<MachinePartStore>();

            services.AddSingleton<MachineProvider>();
            services.AddSingleton<MachineStore>();

            services.AddSingleton<PartProvider>();
            services.AddSingleton<PartsStore>();

            services.AddSingleton<PartTypeProvider>();
            services.AddSingleton<PartTypesStore>();

            services.AddSingleton<RoleProvider>();
            //services.AddSingleton<RoleStore>();

            services.AddSingleton<RoleUserProvider>();
            //services.AddSingleton<RoleUserStore>();

            services.AddSingleton<SupplierProvider>();
            services.AddSingleton<SuppliersStore>();

            services.AddSingleton<TaskTypeProvider>();
            services.AddSingleton<TaskTypeStore>();

            services.AddSingleton<UserProvider>();
            //services.AddSingleton<UserStore>();
        }
    }
}
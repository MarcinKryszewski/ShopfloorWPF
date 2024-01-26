using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.UserModel;
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
            MachinePartServices.GetServices(services);
            MachineServices.GetServices(services);

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

            services.AddSingleton<ErrandTypeProvider>();
            services.AddSingleton<ErrandTypeStore>();

            services.AddSingleton<UserProvider>();
            //services.AddSingleton<UserStore>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleUserModel;

namespace Shopfloor.Hosts.Database
{
    internal class RoleUserServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddTransient<IProvider<RoleUser>, RoleUserProvider>();
            services.AddSingleton<IDataStore<RoleUser>, RoleUserStore>();
            // services.AddSingleton<ICombinerManager<RoleUser>, RoleUserCombiner>();

            services.AddSingleton<IRoleUserProvider, RoleUserProvider>();
        }
    }
}
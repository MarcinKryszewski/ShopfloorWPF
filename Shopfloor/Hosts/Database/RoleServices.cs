using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;

namespace Shopfloor.Hosts.Database
{
    internal class RoleServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Role>, RoleProvider>();
            services.AddSingleton<IDataStore<Role>, RoleStore>();
            // services.AddSingleton<ICombinerManager<Role>, RoleCombiner>();
        }
    }
}
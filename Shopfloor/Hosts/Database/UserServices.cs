using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using Shopfloor.Models.UserModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class UserServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<User>, UserProvider>();
            services.AddSingleton<IDataStore<User>, UserStore>();
            services.AddSingleton<ICombinerManager<User>, UserCombiner>();

            services.AddSingleton<IUserProvider, UserProvider>();
        }
    }
}
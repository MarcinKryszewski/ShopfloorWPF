using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Hosts
{
    internal sealed class UserHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(databaseServices.GetRequiredService<RoleProvider>());
                services.AddSingleton(databaseServices.GetRequiredService<RoleUserProvider>());
                services.AddSingleton(databaseServices.GetRequiredService<UserProvider>());
                services.AddSingleton<CurrentUserStore>();
            })
            .Build();
        }
    }
}
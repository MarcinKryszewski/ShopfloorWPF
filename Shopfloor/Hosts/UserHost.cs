using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Services.Providers;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Hosts
{
    public class UserHost
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
                services.AddSingleton<UserStore>();
            })
            .Build();
        }
    }
}
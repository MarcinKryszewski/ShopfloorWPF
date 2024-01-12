using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Users.Services;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Admin.Users.Hosts
{
    public class UsersHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedUserStore>();

                UsersNavigationService.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
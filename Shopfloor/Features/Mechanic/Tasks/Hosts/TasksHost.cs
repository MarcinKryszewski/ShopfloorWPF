using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Mechanic.Tasks.Services;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Mechanic.Tasks.Hosts
{
    public class TasksHost
    {
        public static IHost GetHost(IServiceProvider databaseServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedUserStore>();

                TasksNavigationServices.Get(services, databaseServices);
            })
            .Build();
        }
    }
}
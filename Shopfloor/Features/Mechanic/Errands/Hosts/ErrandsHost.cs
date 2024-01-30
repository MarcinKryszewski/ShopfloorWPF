using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Mechanic.Errands.Services;
using Shopfloor.Shared.Stores;

namespace Shopfloor.Features.Mechanic.Errands.Hosts
{
    public class ErrandsHost
    {
        public static IHost GetHost(IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedUserStore>();

                ErrandsNavigationServices.Get(services, databaseServices, userServices);
            })
            .Build();
        }
    }
}
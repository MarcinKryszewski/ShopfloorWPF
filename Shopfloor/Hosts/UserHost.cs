using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                services.AddSingleton<UserHost>();
            })
            .Build();
        }
    }
}
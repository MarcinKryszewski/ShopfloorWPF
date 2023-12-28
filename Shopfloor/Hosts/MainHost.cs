using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Layout.SidePanel;

namespace Shopfloor.Hosts.MainHost
{
    public class MainHost
    {
        public static IHost GetHost()
        {
            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<SidePanelViewModel>();
            })
            .Build();
        }
    }
}
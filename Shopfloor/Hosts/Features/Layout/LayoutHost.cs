using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Layout.TopPanel;

namespace Shopfloor.Hosts.Features.Layout
{
    internal static class LayoutHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<SidePanelViewModel>();
            services.AddSingleton<ContentViewModel>();
            services.AddTransient<TopPanelViewModel>();
        }
    }
}
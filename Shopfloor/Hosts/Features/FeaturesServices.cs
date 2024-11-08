using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.ActionsList;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActionsListViewModel>();
            services.AddTransient<ActionsFilter>();
        }
    }
}
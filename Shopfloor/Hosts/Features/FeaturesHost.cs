using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Contexts;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Features.God;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Hosts.Features.Notifier;
using Shopfloor.Hosts.Models;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<WorkInProgressViewModel>();
            services.AddSingleton<GodViewModel>();

            NotifierServices.Get(services);

            Contexts(services);
            ModelServices.Get(services);
            FeaturesServices.Get(services);
        }
        private static void Contexts(IServiceCollection services)
        {
            services.AddSingleton<WorkOrderContext>();
            services.AddSingleton<PartsBasketContext>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;

namespace Shopfloor.Hosts.Features.Plannist
{
    internal static class PlannistHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<PlannistDashboardViewModel>();

            services.AddTransient<PlannistPartsListViewModel>();

            services.AddTransient<DeploysViewModel>();

            services.AddTransient<OffersViewModel>();
            services.AddTransient<AddOfferViewModel>();

            services.AddTransient<PartsOrdersViewModel>();

            services.AddTransient<ReservationsViewModel>();

            services.AddSingleton<SelectedRequestStore>();
        }
    }
}
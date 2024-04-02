using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;

namespace Shopfloor.Hosts.Features.Plannist
{
    internal sealed class PlannistHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<PlannistDashboardViewModel>();

            services.AddSingleton<PlannistPartsListViewModel>();

            services.AddSingleton<DeploysViewModel>();

            services.AddSingleton<OffersViewModel>();
            services.AddSingleton<AddOfferViewModel>();

            services.AddSingleton<PartsOrdersViewModel>();

            services.AddSingleton<ReservationsViewModel>();

            services.AddSingleton<SelectedRequestStore>();
        }
    }
}
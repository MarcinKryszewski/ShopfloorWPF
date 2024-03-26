using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Features.Mechanic.MechanicDashboard;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Requests.Stores;

namespace Shopfloor.Hosts.Features.Mechanic
{
    internal sealed class MechanicHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<MechanicDashboardViewModel>();
            GetErrand(services);
            GetRequest(services);
        }
        private static void GetErrand(IServiceCollection services)
        {
            services.AddSingleton<ErrandsListViewModel>();
            services.AddSingleton<ErrandEditViewModel>();
            services.AddSingleton<ErrandNewViewModel>();
            services.AddSingleton<ErrandPartsListViewModel>();
            services.AddSingleton<SelectedErrandStore>();
        }
        private static void GetRequest(IServiceCollection services)
        {
            services.AddSingleton<RequestsListViewModel>();
            services.AddSingleton<RequestsEditViewModel>();
            services.AddSingleton<RequestsDetailsViewModel>();
            services.AddSingleton<SelectedRequestStore>();
        }
    }
}
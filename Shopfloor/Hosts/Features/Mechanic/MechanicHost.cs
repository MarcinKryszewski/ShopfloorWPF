using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Requests.Stores;

namespace Shopfloor.Hosts.Features.Mechanic
{
    internal sealed class MechanicHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<MechanicDashboardViewModel>();
            services.AddSingleton<PartsStockListViewModel>();
            GetErrand(services);
            GetRequest(services);
        }
        private static void GetErrand(IServiceCollection services)
        {
            services.AddTransient<ErrandsListViewModel>();
            services.AddTransient<ErrandEditViewModel>();
            services.AddTransient<ErrandNewViewModel>();
            services.AddTransient<ErrandPartsListViewModel>();
            services.AddSingleton<SelectedErrandStore>();
        }
        private static void GetRequest(IServiceCollection services)
        {
            services.AddTransient<RequestsListViewModel>();
            services.AddTransient<RequestsEditViewModel>();
            services.AddTransient<RequestsDetailsViewModel>();
            services.AddSingleton<SelectedRequestStore>();
        }
    }
}
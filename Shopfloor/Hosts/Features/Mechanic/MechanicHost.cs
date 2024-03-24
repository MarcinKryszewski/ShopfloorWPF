using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Features.Mechanic.MechanicDashboard;

namespace Shopfloor.Hosts.Features.Mechanic
{
    internal sealed class MechanicHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<MechanicDashboardViewModel>();
            GetErrand(services);
        }
        private static void GetErrand(IServiceCollection services)
        {
            services.AddSingleton<ErrandsListViewModel>();
            services.AddTransient<ErrandEditViewModel>();
            services.AddTransient<ErrandNewViewModel>();
            services.AddTransient<ErrandPartsListViewModel>();
            services.AddSingleton<SelectedErrandStore>();
        }
    }
}
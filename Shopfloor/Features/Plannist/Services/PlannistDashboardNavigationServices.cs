using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.PlannistPartsList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Services
{
    public class PlannistDashboardNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
        }
        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PlannistPartsListViewModel>>((s) => () => s.GetRequiredService<PlannistPartsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PlannistPartsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PlannistPartsListViewModel>>()
                );
            });
        }
        private static PlannistPartsListViewModel CreateListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices) => new(mainServices, databaseServices);
    }
}
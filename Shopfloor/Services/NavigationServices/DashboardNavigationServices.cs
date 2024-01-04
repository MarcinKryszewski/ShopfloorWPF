using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Dashboard;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    public class DashboardNavigationServices
    {
        public static void Get(IServiceCollection services)
        {
            GetDashboardNavigation(services);
        }

        public static void GetDashboardNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateDashboardViewModel(s));
            services.AddSingleton<CreateViewModel<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<DashboardViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<DashboardViewModel>>()
                );
            });
        }

        private static DashboardViewModel CreateDashboardViewModel(IServiceProvider services)
        {
            return new DashboardViewModel(services);
        }
    }
}
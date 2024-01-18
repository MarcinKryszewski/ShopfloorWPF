using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Suppliers.Services
{
    public class SuppliersNavigationService
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
        }

        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateSuppliersListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<SuppliersListViewModel>>((s) => () => s.GetRequiredService<SuppliersListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<SuppliersListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<SuppliersListViewModel>>()
                );
            });
        }

        private static SuppliersListViewModel CreateSuppliersListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new SuppliersListViewModel(databaseServices);
        }
    }
}
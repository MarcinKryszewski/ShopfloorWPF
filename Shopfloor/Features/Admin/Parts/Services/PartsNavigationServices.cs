using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Parts.Services
{
    public class PartsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
        }

        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsListViewModel>>((s) => () => s.GetRequiredService<PartsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsListViewModel>>()
                );
            });
        }

        private static PartsListViewModel CreatePartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartsListViewModel(databaseServices);
        }
    }
}
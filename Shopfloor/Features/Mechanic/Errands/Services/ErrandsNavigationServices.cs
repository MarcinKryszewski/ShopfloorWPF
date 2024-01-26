using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.Services
{
    public class ErrandsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
            //GetAddNavigation(services, databaseServices);
            //GetEditNavigation(services, databaseServices);
        }
        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateTasksListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<ErrandsListViewModel>>((s) => () => s.GetRequiredService<ErrandsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsListViewModel>>()
                );
            });
        }
        private static ErrandsListViewModel CreateTasksListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new ErrandsListViewModel(mainServices, databaseServices);
        }
    }
}
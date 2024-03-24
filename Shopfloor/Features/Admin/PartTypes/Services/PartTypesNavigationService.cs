using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shopfloor.Features.Admin.PartTypes.Services
{
    internal sealed class PartTypesNavigationService
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            //GetListNavigation(services, databaseServices);
        }

        /*private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartTypesListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartTypesListViewModel>>((s) => () => s.GetRequiredService<PartTypesListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartTypesListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartTypesListViewModel>>()
                );
            });
        }

        private static PartTypesListViewModel CreatePartTypesListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartTypesListViewModel(databaseServices);
        }*/
    }
}
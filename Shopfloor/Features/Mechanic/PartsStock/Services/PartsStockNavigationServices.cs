using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shopfloor.Features.Mechanic.PartsStock.Services
{
    internal sealed class PartsStockNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            //GetListNavigation(services, databaseServices);
        }
        /*private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsStockListViewModel>>((s) => () => s.GetRequiredService<PartsStockListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsStockListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsStockListViewModel>>()
                );
            });
        }
        private static PartsStockListViewModel CreateListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartsStockListViewModel(mainServices, databaseServices);
        }*/
    }
}
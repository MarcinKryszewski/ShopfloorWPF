using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.RequestsList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests.Services
{
    internal sealed class RequestsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
        }
        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<RequestsListViewModel>>((s) => () => s.GetRequiredService<RequestsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsListViewModel>>()
                );
            });
        }
        private static RequestsListViewModel CreateListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices) => new RequestsListViewModel(mainServices, databaseServices);
    }
}
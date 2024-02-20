using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.RequestsDetails;
using Shopfloor.Features.Mechanic.Requests.RequestsEdit;
using Shopfloor.Features.Mechanic.Requests.RequestsList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests.Services
{
    internal sealed class RequestsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            GetListNavigation(services, databaseServices, userServices);
            GetDetailsNavigation(services, databaseServices);
            GetEditNavigation(services, databaseServices);
        }
        private static void GetDetailsNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateDetailsViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<RequestsDetailsViewModel>>((s) => () => s.GetRequiredService<RequestsDetailsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsDetailsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsDetailsViewModel>>()
                );
            });
        }
        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateListViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<RequestsListViewModel>>((s) => () => s.GetRequiredService<RequestsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsListViewModel>>()
                );
            });
        }
        private static void GetEditNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateEditViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<RequestsEditViewModel>>((s) => () => s.GetRequiredService<RequestsEditViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsEditViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsEditViewModel>>()
                );
            });
        }
        private static RequestsListViewModel CreateListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices) => new(mainServices, databaseServices, userServices);
        private static RequestsEditViewModel CreateEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices) => new();
        private static RequestsDetailsViewModel CreateDetailsViewModel(IServiceProvider mainServices, IServiceProvider databaseServices) => new(mainServices, databaseServices);
    }
}
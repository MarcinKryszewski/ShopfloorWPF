using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.MinimalStates;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class MechanicNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            GetTasksNavigation(services, databaseServices, userServices);
            GetRequestsNavigation(services, databaseServices, userServices);
            GetMinimalStatesNavigation(services);
        }
        private static void GetMinimalStatesNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateMinimalStatesViewModel(s));
            services.AddSingleton<CreateViewModel<MinimalStatesViewModel>>((s) => () => s.GetRequiredService<MinimalStatesViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<MinimalStatesViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<MinimalStatesViewModel>>()
                );
            });
        }
        private static void GetRequestsNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreatRequestsViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<RequestsMainViewModel>>((s) => () => s.GetRequiredService<RequestsMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsMainViewModel>>()
                );
            });
        }
        private static void GetTasksNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreatTasksViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<ErrandsMainViewModel>>((s) => () => s.GetRequiredService<ErrandsMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsMainViewModel>>()
                );
            });
        }

        private static MinimalStatesViewModel CreateMinimalStatesViewModel(IServiceProvider services) => new MinimalStatesViewModel();
        private static RequestsMainViewModel CreatRequestsViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new RequestsMainViewModel(databaseServices, userServices);
        private static ErrandsMainViewModel CreatTasksViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new(databaseServices, userServices);
    }
}
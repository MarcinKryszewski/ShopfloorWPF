using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.MinimalStates;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Tasks;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    public class MechanicNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetTasksNavigation(services, databaseServices);
            GetRequestsNavigation(services);
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

        private static void GetRequestsNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreatRequestsViewModel(s));
            services.AddSingleton<CreateViewModel<RequestsViewModel>>((s) => () => s.GetRequiredService<RequestsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsViewModel>>()
                );
            });
        }

        private static void GetTasksNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatTasksViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<TasksMainViewModel>>((s) => () => s.GetRequiredService<TasksMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<TasksMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<TasksMainViewModel>>()
                );
            });
        }

        private static MinimalStatesViewModel CreateMinimalStatesViewModel(IServiceProvider services)
        {
            return new MinimalStatesViewModel();
        }

        private static RequestsViewModel CreatRequestsViewModel(IServiceProvider services)
        {
            return new RequestsViewModel();
        }

        private static TasksMainViewModel CreatTasksViewModel(IServiceProvider services, IServiceProvider databaseServices)
        {
            return new TasksMainViewModel(databaseServices);
        }
    }
}
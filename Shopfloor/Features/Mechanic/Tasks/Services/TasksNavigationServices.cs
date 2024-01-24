using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Tasks.TasksList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Tasks.Services
{
    public class TasksNavigationServices
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
            services.AddSingleton<CreateViewModel<TasksListViewModel>>((s) => () => s.GetRequiredService<TasksListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<TasksListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<TasksListViewModel>>()
                );
            });
        }
        private static TasksListViewModel CreateTasksListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new TasksListViewModel(mainServices, databaseServices);
        }
    }
}
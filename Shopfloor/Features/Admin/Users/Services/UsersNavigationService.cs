using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.Add;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Users.Services
{
    public class UsersNavigationService
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
            GetAddNavigation(services, databaseServices);
        }

        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateUsersListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<UsersListViewModel>>((s) => () => s.GetRequiredService<UsersListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<UsersListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<UsersListViewModel>>()
                );
            });
        }
        private static void GetAddNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateUsersAddViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<UsersAddViewModel>>((s) => () => s.GetRequiredService<UsersAddViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<UsersAddViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<UsersAddViewModel>>()
                );
            });
        }

        private static UsersListViewModel CreateUsersListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new UsersListViewModel(mainServices, databaseServices);
        }
        private static UsersAddViewModel CreateUsersAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new UsersAddViewModel(mainServices, databaseServices);
        }
    }
}
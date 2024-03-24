using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shopfloor.Features.Admin.Users.Services
{
    internal sealed class UsersNavigationService
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            //GetListNavigation(services, databaseServices);
            //GetAddNavigation(services, databaseServices);
            //GetEditNavigation(services, databaseServices);
        }

        /*private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
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

        private static void GetEditNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateUsersEditViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<UsersEditViewModel>>((s) => () => s.GetRequiredService<UsersEditViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<UsersEditViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<UsersEditViewModel>>()
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

        private static UsersEditViewModel CreateUsersEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new UsersEditViewModel(mainServices, databaseServices);
        }*/
    }
}
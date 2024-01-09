using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    public class AdminNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetUsersNavigation(services);
            GetMachinesNavigation(services);
            GetPartsNavigation(services, databaseServices);
        }

        public static void GetUsersNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateUsersViewModel(s));
            services.AddSingleton<CreateViewModel<UsersViewModel>>((s) => () => s.GetRequiredService<UsersViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<UsersViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<UsersViewModel>>()
                );
            });
        }
        public static void GetMachinesNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateMachinesViewModel(s));
            services.AddSingleton<CreateViewModel<MachinesViewModel>>((s) => () => s.GetRequiredService<MachinesViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<MachinesViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<MachinesViewModel>>()
                );
            });
        }
        public static void GetPartsNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsViewModel(databaseServices));
            services.AddSingleton<CreateViewModel<PartsViewModel>>((s) => () => s.GetRequiredService<PartsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsViewModel>>()
                );
            });
        }

        private static UsersViewModel CreateUsersViewModel(IServiceProvider services)
        {
            return new UsersViewModel();
        }
        private static MachinesViewModel CreateMachinesViewModel(IServiceProvider services)
        {
            return new MachinesViewModel();
        }
        private static PartsViewModel CreatePartsViewModel(IServiceProvider databaseServices)
        {
            return new PartsViewModel(databaseServices);
        }
    }
}
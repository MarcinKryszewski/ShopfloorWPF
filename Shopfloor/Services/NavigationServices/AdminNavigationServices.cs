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
            GetUsersNavigation(services, databaseServices);
            GetMachinesNavigation(services);
            GetPartsNavigation(services, databaseServices);
        }

        public static void GetUsersNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateUsersMainViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<UsersMainViewModel>>((s) => () => s.GetRequiredService<UsersMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<UsersMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<UsersMainViewModel>>()
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

        private static UsersMainViewModel CreateUsersMainViewModel(IServiceProvider services, IServiceProvider databaseServices)
        {
            return new UsersMainViewModel(databaseServices);
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
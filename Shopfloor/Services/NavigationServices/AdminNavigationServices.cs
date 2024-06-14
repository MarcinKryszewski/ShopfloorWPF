using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class AdminNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetUsersNavigation(services, databaseServices);
            GetMachinesNavigation(services, databaseServices);
            GetPartsNavigation(services, databaseServices);
            GetSuppliersNavigation(services, databaseServices);
            GetPartTypesNavigation(services, databaseServices);
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

        public static void GetMachinesNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateMachinesViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<MachinesMainViewModel>>((s) => () => s.GetRequiredService<MachinesMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<MachinesMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<MachinesMainViewModel>>()
                );
            });
        }

        public static void GetPartsNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsMainViewModel(databaseServices));
            services.AddSingleton<CreateViewModel<PartsMainViewModel>>((s) => () => s.GetRequiredService<PartsMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsMainViewModel>>()
                );
            });
        }

        public static void GetSuppliersNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateSuppliersViewModel(databaseServices));
            services.AddSingleton<CreateViewModel<SuppliersMainViewModel>>((s) => () => s.GetRequiredService<SuppliersMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<SuppliersMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<SuppliersMainViewModel>>()
                );
            });
        }

        public static void GetPartTypesNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartTypesViewModel(databaseServices));
            services.AddSingleton<CreateViewModel<PartTypesMainViewModel>>((s) => () => s.GetRequiredService<PartTypesMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartTypesMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartTypesMainViewModel>>()
                );
            });
        }

        private static UsersMainViewModel CreateUsersMainViewModel(IServiceProvider services, IServiceProvider databaseServices)
        {
            return new UsersMainViewModel(databaseServices);
        }

        private static MachinesMainViewModel CreateMachinesViewModel(IServiceProvider services, IServiceProvider databaseServices)
        {
            return new MachinesMainViewModel(databaseServices);
        }

        private static PartsMainViewModel CreatePartsMainViewModel(IServiceProvider databaseServices)
        {
            return new PartsMainViewModel(databaseServices);
        }

        private static SuppliersMainViewModel CreateSuppliersViewModel(IServiceProvider databaseServices)
        {
            return new SuppliersMainViewModel(databaseServices);
        }

        private static PartTypesMainViewModel CreatePartTypesViewModel(IServiceProvider databaseServices)
        {
            return new PartTypesMainViewModel(databaseServices);
        }
    }
}
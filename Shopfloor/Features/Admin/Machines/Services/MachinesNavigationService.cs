using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Machines.Services
{
    internal sealed class MachinesNavigationService
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            GetListNavigation(services, databaseServices);
        }

        private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateMachinesListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<MachinesListViewModel>>((s) => () => s.GetRequiredService<MachinesListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<MachinesListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<MachinesListViewModel>>()
                );
            });
        }

        private static MachinesListViewModel CreateMachinesListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new MachinesListViewModel(mainServices, databaseServices);
        }
    }
}
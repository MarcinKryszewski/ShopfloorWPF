using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    public class AdminNavigationServices
    {
        public AdminNavigationServices(IServiceProvider services)
        {

        }
        public void GetUsersNavigation(IServiceCollection services)
        {
            NewMethod<UsersViewModel>(services, CreateUsersViewModel);
        }
        public void GetMachinesNavigation(IServiceCollection services)
        {
            NewMethod<MachinesViewModel>(services, CreateMachinesViewModel);
        }
        public void GetPartsNavigation(IServiceCollection services)
        {
            NewMethod<PartsViewModel>(services, CreatePartsViewModel);
        }

        private void NewMethod<T>(IServiceCollection services, Func<IServiceProvider, ViewModelBase> createViewModel) where T : ViewModelBase
        {
            services.AddTransient((s) => createViewModel(s));
            services.AddSingleton<CreateViewModel<T>>((s) => () => s.GetRequiredService<T>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<T>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<T>>()
                );
            });
        }

        private UsersViewModel CreateUsersViewModel(IServiceProvider services)
        {
            return new UsersViewModel();
        }
        private MachinesViewModel CreateMachinesViewModel(IServiceProvider services)
        {
            return new MachinesViewModel();
        }
        private PartsViewModel CreatePartsViewModel(IServiceProvider services)
        {
            return new PartsViewModel();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Features.Admin.Users.Hosts;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Machines
{
    public class MachinesMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _machinesServices;

        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public MachinesMainViewModel(IServiceProvider databasServices)
        {
            _machinesServices = UsersHost.GetHost(databasServices);
            _machinesServices.Start();

            _navigationStore = _machinesServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<MachinesListViewModel> navigationService = _machinesServices.Services.GetRequiredService<NavigationService<MachinesListViewModel>>();
            navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Users.Hosts;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Users
{
    internal sealed class UsersMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _usersServices;

        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public UsersMainViewModel(IServiceProvider databasServices)
        {
            _usersServices = UsersHost.GetHost(databasServices);
            _usersServices.Start();

            _navigationStore = _usersServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<UsersListViewModel> navigationService = _usersServices.Services.GetRequiredService<NavigationService<UsersListViewModel>>();
            navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
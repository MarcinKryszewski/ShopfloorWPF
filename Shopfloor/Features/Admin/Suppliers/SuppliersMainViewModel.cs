using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Suppliers.Hosts;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Suppliers
{
    internal sealed class SuppliersMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _suppliersServices;

        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public SuppliersMainViewModel(IServiceProvider databaseServices)
        {
            _suppliersServices = SuppliersHost.GetHost(databaseServices);
            _suppliersServices.Start();

            _navigationStore = _suppliersServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //NavigationService<SuppliersListViewModel> navigationService = _suppliersServices.Services.GetRequiredService<NavigationService<SuppliersListViewModel>>();
            //navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
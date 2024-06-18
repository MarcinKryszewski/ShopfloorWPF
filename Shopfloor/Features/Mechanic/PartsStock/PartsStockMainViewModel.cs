using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.PartsStock.Hosts;
using Shopfloor.Features.Mechanic.PartsStock.PartsStockList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.PartsStock
{
    internal sealed class PartsStockMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _partsStockServices;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public PartsStockMainViewModel(IServiceProvider databaseServices)
        {
            _partsStockServices = PartsStockHost.GetHost(databaseServices);
            _partsStockServices.Start();

            _navigationStore = _partsStockServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<PartsStockListViewModel> navigationService = _partsStockServices.Services.GetRequiredService<NavigationService<PartsStockListViewModel>>();
            navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
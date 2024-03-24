using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.Parts.Hosts;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.Parts
{
    internal sealed class PartsMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _partsServices;

        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public PartsMainViewModel(IServiceProvider databaseServices)
        {
            _partsServices = PartsHost.GetHost(databaseServices);
            _partsServices.Start();

            _navigationStore = _partsServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //NavigationService<PartsListViewModel> navigationService = _partsServices.Services.GetRequiredService<NavigationService<PartsListViewModel>>();
            //navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts
{
    public class PartsMainViewModel : ViewModelBase
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

            NavigationService<PartsListViewModel> navigationService = _partsServices.Services.GetRequiredService<NavigationService<PartsListViewModel>>();
            navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
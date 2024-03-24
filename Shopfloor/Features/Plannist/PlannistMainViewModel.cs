using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Plannist.PlannistDashboard.Hosts;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Plannist
{
    internal sealed class PlannistMainViewModel : ViewModelBase
    {
        public readonly NavigationStore _navigationStore;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        private IServiceProvider _services;
        public PlannistMainViewModel(IServiceProvider databaseServices)
        {
            IHost host = PlannistDashboardHost.GetHost(databaseServices);
            host.Start();
            _services = host.Services;

            _navigationStore = _services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //NavigationService<PlannistPartsListViewModel> navigationService = services.GetRequiredService<NavigationService<PlannistPartsListViewModel>>();
            //navigationService.Navigate();
        }
        public void NavigationService(ViewModelBase viewModel)
        {
            //NavigationService<ViewModelBase> navigationService = _services.GetRequiredService<NavigationService<ViewModelBase>>();
            //navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged() => OnPropertyChanged(nameof(Content));
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.Requests.Hosts;
using Shopfloor.Features.Plannist.PlannistDashboard.Hosts;
using Shopfloor.Features.Plannist.PlannistDashboard.PlannistPartsList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Plannist.PlannistDashboard
{
    internal sealed class PlannistDashboardMainViewModel : ViewModelBase
    {
        public readonly NavigationStore _navigationStore;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public PlannistDashboardMainViewModel(IServiceProvider databaseServices)
        {
            IHost host = PlannistDashboardHost.GetHost(databaseServices);
            host.Start();
            IServiceProvider services = host.Services;

            _navigationStore = services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<PlannistPartsListViewModel> navigationService = services.GetRequiredService<NavigationService<PlannistPartsListViewModel>>();
            navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged() => OnPropertyChanged(nameof(Content));
    }
}
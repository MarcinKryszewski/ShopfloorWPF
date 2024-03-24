using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.Requests.Hosts;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Mechanic.Requests
{
    internal sealed class RequestsMainViewModel : ViewModelBase
    {
        public readonly NavigationStore _navigationStore;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public RequestsMainViewModel(IServiceProvider databaseServices, IServiceProvider userServices)
        {
            IHost host = RequestsHost.GetHost(databaseServices, userServices);
            host.Start();
            IServiceProvider services = host.Services;

            _navigationStore = services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //NavigationService<RequestsListViewModel> navigationService = services.GetRequiredService<NavigationService<RequestsListViewModel>>();
            //navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged() => OnPropertyChanged(nameof(Content));
    }
}
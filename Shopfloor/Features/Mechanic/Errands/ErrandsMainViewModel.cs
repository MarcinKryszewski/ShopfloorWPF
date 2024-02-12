using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.Errands.ErrandsList;
using Shopfloor.Features.Mechanic.Errands.Hosts;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Mechanic.Errands
{
    public class ErrandsMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _errandsServices;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public ErrandsMainViewModel(IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _errandsServices = ErrandsHost.GetHost(databaseServices, userServices);
            _errandsServices.Start();

            _navigationStore = _errandsServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<ErrandsListViewModel> navigationService = _errandsServices.Services.GetRequiredService<NavigationService<ErrandsListViewModel>>();
            navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
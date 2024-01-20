using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Admin.PartTypes.Hosts;
using Shopfloor.Features.Admin.PartTypes.List;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Features.Admin.PartTypes
{
    public class PartTypesMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _partTypesServices;

        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public PartTypesMainViewModel(IServiceProvider databaseServices)
        {
            _partTypesServices = PartTypesHost.GetHost(databaseServices);
            _partTypesServices.Start();

            _navigationStore = _partTypesServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<PartTypesListViewModel> navigationService = _partTypesServices.Services.GetRequiredService<NavigationService<PartTypesListViewModel>>();
            navigationService.Navigate();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}
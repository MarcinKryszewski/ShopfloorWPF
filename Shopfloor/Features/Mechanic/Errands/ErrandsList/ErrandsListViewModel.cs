using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private List<Errand> _errands = [];
        private readonly ErrandStore _errandStore;
        private readonly ErrandCombiner _errandCombiner;
        private readonly ErrandPartCombiner _errandPartCombiner;

        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Errand? SelectedErrand { get; set; }
        public ICommand EditErrandCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ErrandsListViewModel(NavigationService navigationService, CurrentUserStore currentUserStore, ErrandStore errandStore, ErrandCombiner errandCombiner, ErrandPartCombiner errandPartCombiner)
        {
            _errandStore = errandStore;
            _errandCombiner = errandCombiner;
            _errandPartCombiner = errandPartCombiner;
            LoadData();
            ErrandsAddNavigateCommand = new RelayCommand(o => { navigationService.NavigateTo<ErrandNewViewModel>(); }, o => true);
            EditErrandCommand = new RelayCommand(o => { navigationService.NavigateTo<ErrandEditViewModel>(); }, o => true);
            if (currentUserStore.User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
        }
        private Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_errands.Clear);

            _errandPartCombiner.Combine().Wait();
            _errandCombiner.Combine().Wait();

            _errands = _errandStore.Data;

            Application.Current.Dispatcher.Invoke(Errands.Refresh);

            return Task.CompletedTask;
        }
    }
}
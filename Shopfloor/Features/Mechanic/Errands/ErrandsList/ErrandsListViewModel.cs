using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Services;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private readonly ICombinerManager<Errand> _errandCombiner;
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<Errand> _errandStore;
        private readonly SelectedErrandStore _selectedErrandStore;
        private List<Errand> _errands = [];
        public ErrandsListViewModel(
            NavigationService navigationService,
            StoreRepository stores,
            ICombinerManager<Errand> errandCombiner,
            ICombinerManager<ErrandPart> errandPartCombiner)
        {
            _errandStore = stores.Errand;
            _errandCombiner = errandCombiner;
            _errandPartCombiner = errandPartCombiner;
            _selectedErrandStore = stores.SelectedErrand;
            LoadData();

            ErrandsAddNavigateCommand = new NavigationCommand<ErrandNewViewModel>(navigationService).Navigate();
            EditErrandCommand = new NavigationCommand<ErrandEditViewModel>(navigationService).Navigate();
            if (stores.CurrentUser.User?.IsAuthorized(568) ?? false)
            {
                HasAccess = Visibility.Visible;
            }
        }
        public ICommand EditErrandCommand { get; }
        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public Errand? SelectedErrand
        {
            get => _selectedErrandStore.SelectedErrand;
            set => _selectedErrandStore.SelectedErrand = value;
        }
        private Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_errands.Clear);

            _errandPartCombiner.CombineAll().Wait();
            _errandCombiner.CombineAll().Wait();

            _errands = _errandStore.Data;

            Application.Current.Dispatcher.Invoke(Errands.Refresh);

            return Task.CompletedTask;
        }
    }
}
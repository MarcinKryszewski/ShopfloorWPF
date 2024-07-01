using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Services.NavigationServices;
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
        private readonly IDataStore<Errand> _errandStore;
        private readonly ICombinerManager<Errand> _errandCombiner;
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;

        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Errand? SelectedErrand { get; set; }
        public ICommand EditErrandCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ErrandsListViewModel(NavigationService navigationService, ICurrentUserStore currentUserStore, IDataStore<Errand> errandStore, ICombinerManager<Errand> errandCombiner, ICombinerManager<ErrandPart> errandPartCombiner)
        {
            _errandStore = errandStore;
            _errandCombiner = errandCombiner;
            _errandPartCombiner = errandPartCombiner;
            LoadData();
            ErrandsAddNavigateCommand = new NavigationCommand<ErrandNewViewModel>(navigationService).Navigate();
            EditErrandCommand = new NavigationCommand<ErrandEditViewModel>(navigationService).Navigate();
            if (currentUserStore.User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
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
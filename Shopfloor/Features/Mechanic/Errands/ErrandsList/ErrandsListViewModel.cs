using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private readonly List<Errand> _errands = [];
        private readonly PartStore _partsStore;
        private readonly ErrandStore _errandStore;
        private readonly ErrandPartStore _errandPartStore;

        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Errand? SelectedErrand { get; set; }
        public ICommand EditErrandCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ErrandsListViewModel(NavigationService navigationService, CurrentUserStore currentUserStore, ErrandStore errandStore, ErrandPartStore errandPartStore, PartStore partsStore)
        {
            _partsStore = partsStore;
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;

            Task.Run(LoadData);
            ErrandsAddNavigateCommand = new RelayCommand(o => { navigationService.NavigateTo<ErrandNewViewModel>(); }, o => true);
            EditErrandCommand = new RelayCommand(o => { navigationService.NavigateTo<ErrandEditViewModel>(); }, o => true);
            if (currentUserStore.User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_errands.Clear);

            await LoadStores(_errandStore, _partsStore);
            await CombineData(_errandStore, _errandPartStore);
            await FillErrandList(_errandStore);

            Application.Current.Dispatcher.Invoke(Errands.Refresh);
        }
        private async Task LoadStores(ErrandStore errandStore, PartStore partsStore)
        {
            List<Task> tasks = [];

            if (!errandStore.IsLoaded) tasks.Add(LoadStore(errandStore));
            if (!partsStore.IsLoaded) tasks.Add(LoadStore(partsStore));

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private static Task LoadStore<T>(IDataStore<T> dataStore)
        {
            dataStore.Load();
            return Task.CompletedTask;
        }
        private async Task CombineData(ErrandStore errandStore, ErrandPartStore errandParts)
        {
            List<Task> tasks = [];

            tasks.Add(errandStore.CombineData());
            tasks.Add(errandParts.CombineData());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task FillErrandList(ErrandStore errandStore)
        {
            _errands.AddRange(from Errand errand in errandStore.GetData select errand);
            return Task.CompletedTask;
        }
    }
}
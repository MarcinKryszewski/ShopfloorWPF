using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsList
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly List<Errand> _errands = [];
        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Errand? SelectedErrand { get; set; }
        public ICommand EditErrandCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ErrandsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
            Task.Run(LoadData);
            ErrandsAddNavigateCommand = new NavigateCommand<ErrandsNewViewModel>(_mainServices.GetRequiredService<NavigationService<ErrandsNewViewModel>>());
            EditErrandCommand = new ErrandSetCommand(this, _mainServices);
            if (userServices.GetRequiredService<CurrentUserStore>().User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_errands.Clear);

            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();

            await LoadStores(errandStore, partsStore);
            await CombineData(errandStore, errandPartStore);
            await FillErrandList(errandStore);

            Application.Current.Dispatcher.Invoke(Errands.Refresh);
        }
        private async Task LoadStores(ErrandStore errandStore, PartsStore partsStore)
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
            _errands.AddRange(from Errand errand in errandStore.Data select errand);
            return Task.CompletedTask;
        }
    }
}
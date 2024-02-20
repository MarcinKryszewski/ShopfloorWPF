using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.RequestsDetails;
using Shopfloor.Features.Mechanic.Requests.RequestsEdit;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;

namespace Shopfloor.Features.Mechanic.Requests.RequestsList
{
    internal sealed class RequestsListViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _parts = [];
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly SelectedRequestStore _requestStore;
        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICommand EditCommand { get; }
        public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public RequestsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            Task.Run(LoadData);

            _requestStore = _mainServices.GetRequiredService<SelectedRequestStore>();
            SelectedRow = null;

            EditCommand = new NavigateCommand<RequestsEditViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsEditViewModel>>());
            DetailsCommand = new NavigateCommand<RequestsDetailsViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsDetailsViewModel>>());

            if (userServices.GetRequiredService<CurrentUserStore>().User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
        }
        #region LOAD_DATA
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_parts.Clear);

            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStatusStore partsStatusStore = _databaseServices.GetRequiredService<ErrandPartStatusStore>();
            PartTypesStore partTypesStore = _databaseServices.GetRequiredService<PartTypesStore>();

            await LoadStores(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore, partTypesStore);
            await CombineData(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore, partTypesStore);
            await FillLists(errandPartStore);

            Application.Current.Dispatcher.Invoke(Parts.Refresh);
        }
        #region FETCH_DATA
        private static async Task LoadStores(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore, PartTypesStore partTypesStore)
        {
            List<Task> tasks = [];
            if (!errandStore.IsLoaded) tasks.Add(LoadStore(errandStore));
            if (!userStore.IsLoaded) tasks.Add(LoadStore(userStore));
            if (!machineStore.IsLoaded) tasks.Add(LoadStore(machineStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadStore(errandPartStore));
            if (!partsStore.IsLoaded) tasks.Add(LoadStore(partsStore));
            if (!partsStatusStore.IsLoaded) tasks.Add(LoadStore(partsStatusStore));
            if (!partTypesStore.IsLoaded) tasks.Add(LoadStore(partTypesStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private static Task LoadStore<T>(IDataStore<T> dataStore)
        {
            dataStore.Load();
            return Task.CompletedTask;
        }
        #endregion FETCH_DATA        
        #region COMBINE_DATA
        private Task CombineData(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore, PartTypesStore partTypesStore)
        {
            CombineErrandWithMachine(errandStore, machineStore);
            CombineErrandPartWithErrand(errandStore, errandPartStore);
            CombineErrandPartWithPart(errandPartStore, partsStore);
            CombineErrandPartWithStatus(errandPartStore, partsStatusStore);
            CombinePartWithType(partsStore, partTypesStore);
            CombineErrandPartWithUser(errandPartStore, userStore);
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithStatus(ErrandPartStore errandPartStore, ErrandPartStatusStore partsStatusStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(partsStatusStore.Data.Where(p => p.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private static Task CombineErrandWithMachine(ErrandStore errandStore, MachineStore machineStore)
        {
            foreach (Errand errand in errandStore.Data)
            {
                errand.Machine = machineStore.Data.FirstOrDefault(m => m.Id == errand.MachineId);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithErrand(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.Errand = errandStore.Data.FirstOrDefault(e => e.Id == errandPart.ErrandId);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithUser(ErrandPartStore errandPartStore, UserStore userStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.OrderedByUser = userStore.Data.FirstOrDefault(u => u.Id == errandPart.OrderedById);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithPart(ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.Part = partsStore.Data.FirstOrDefault(p => p.Id == errandPart.PartId);
            }
            return Task.CompletedTask;
        }
        private Task CombinePartWithType(PartsStore partsStore, PartTypesStore partTypesStore)
        {
            foreach (Part part in partsStore.Data)
            {
                part.SetType(partTypesStore.Data.FirstOrDefault(type => type.Id == part.TypeId));
            }
            return Task.CompletedTask;
        }
        #endregion COMBINE_DATA        
        #region FILL_LISTS
        private async Task FillLists(ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillPartList(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task FillPartList(ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                _parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        #endregion FILL_LISTS
        #endregion LOAD_DATA
    }
}
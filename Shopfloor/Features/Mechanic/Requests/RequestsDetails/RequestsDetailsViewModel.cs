using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Mechanic.Requests.RequestsDetails
{
    internal sealed class RequestsDetailsViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly SelectedRequestStore _selectedRequest;
        public ICommand ReturnCommand { get; }
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public RequestsDetailsViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            Task.Run(() => LoadData(databaseServices));
            _selectedRequest = _mainServices.GetRequiredService<SelectedRequestStore>();
            //ReturnCommand = new NavigateCommand<RequestsListViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsListViewModel>>());
        }
        private async Task LoadData(IServiceProvider databaseServices)
        {
            SuppliersStore suppliers = databaseServices.GetRequiredService<SuppliersStore>();
            UserStore users = databaseServices.GetRequiredService<UserStore>();
            PartStore parts = databaseServices.GetRequiredService<PartStore>();
            ErrandPartStatusStore errandPartStatuses = databaseServices.GetRequiredService<ErrandPartStatusStore>();
            ErrandStore errands = databaseServices.GetRequiredService<ErrandStore>();
            ErrandTypeStore errandTypes = databaseServices.GetRequiredService<ErrandTypeStore>();
            ErrandPartStore errandPartStore = databaseServices.GetRequiredService<ErrandPartStore>();

            await LoadStores(suppliers, users, parts, errandPartStatuses, errands, errandTypes, errandPartStore);
            await CombineData(suppliers, users, parts, errandPartStatuses, errands, errandTypes, errandPartStore);

            LoadHistoricalData(errandPartStore);
        }
        #region FETCH_DATA
        private async Task LoadStores(SuppliersStore suppliers, UserStore users, PartStore parts, ErrandPartStatusStore errandPartStatuses, ErrandStore errands, ErrandTypeStore errandTypes, ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            if (!suppliers.IsLoaded) tasks.Add(LoadStore(suppliers));
            if (!users.IsLoaded) tasks.Add(LoadStore(users));
            if (!parts.IsLoaded) tasks.Add(LoadStore(parts));
            if (!errandPartStatuses.IsLoaded) tasks.Add(LoadStore(errandPartStatuses));
            if (!errands.IsLoaded) tasks.Add(LoadStore(errands));
            if (!errandTypes.IsLoaded) tasks.Add(LoadStore(errandTypes));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadStore(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private static Task LoadStore<T>(IDataStore<T> dataStore)
        {
            dataStore.Load();
            return Task.CompletedTask;
        }
        #endregion FETCH_DATA
        #region COMBINE_DATA
        private async Task CombineData(SuppliersStore suppliers, UserStore users, PartStore parts, ErrandPartStatusStore statuses, ErrandStore errands, ErrandTypeStore errandTypes, ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(CombinePartWithSuppliers(parts, suppliers));
            tasks.Add(CombineStatusWithPerson(statuses, users));
            tasks.Add(CombineErrandWithTypePerson(errands, errandTypes, users));
            tasks.Add(CombineErrandWithParts(parts, errands, errandPartStore));
            await Task.WhenAll(tasks);
        }
        private static Task CombinePartWithSuppliers(PartStore parts, SuppliersStore suppliers)
        {
            foreach (Part part in parts.GetData)
            {
                part.SetSupplier(suppliers.GetData.FirstOrDefault(supplier => supplier.Id == part.SupplierId));
                part.SetProducer(suppliers.GetData.FirstOrDefault(producer => producer.Id == part.ProducerId));
            }
            return Task.CompletedTask;
        }
        private Task CombineStatusWithPerson(ErrandPartStatusStore statuses, UserStore users)
        {
            foreach (ErrandPartStatus status in statuses.GetData)
            {
                status.CompletedBy = users.GetData.FirstOrDefault(user => user.Id == status.CompletedById);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandWithParts(PartStore parts, ErrandStore errands, ErrandPartStore errandParts)
        {
            _selectedRequest.Request!.Errand!.Parts.Clear();
            foreach (ErrandPart errandPart in errandParts.GetData)
            {
                if (errandPart.ErrandId != _selectedRequest.Request!.ErrandId) continue;
                _selectedRequest.Request!.Errand!.Parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandWithTypePerson(ErrandStore errands, ErrandTypeStore types, UserStore users)
        {
            foreach (Errand errand in errands.GetData)
            {
                errand.Type = types.GetData.FirstOrDefault(type => type.Id == errand.TypeId);
                errand.CreatedByUser = users.GetData.FirstOrDefault(user => user.Id == errand.CreatedById);
            }
            return Task.CompletedTask;
        }
        #endregion COMBINE_DATA
        private void LoadHistoricalData(ErrandPartStore errandParts)
        {
            HistoricalData = errandParts.GetData.Where(part => part.PartId == _selectedRequest.Request!.PartId);
        }
    }
}
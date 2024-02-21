using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests.RequestsDetails
{
    internal sealed class RequestsDetailsViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly SelectedRequestStore _selectedRequest;
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public RequestsDetailsViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            Task.Run(() => LoadData(databaseServices));
            _selectedRequest = _mainServices.GetRequiredService<SelectedRequestStore>();
        }
        private async Task LoadData(IServiceProvider databaseServices)
        {
            SuppliersStore suppliers = databaseServices.GetRequiredService<SuppliersStore>();
            UserStore users = databaseServices.GetRequiredService<UserStore>();
            PartsStore parts = databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStatusStore errandPartStatuses = databaseServices.GetRequiredService<ErrandPartStatusStore>();
            ErrandStore errands = databaseServices.GetRequiredService<ErrandStore>();
            ErrandTypeStore errandTypes = databaseServices.GetRequiredService<ErrandTypeStore>();
            ErrandPartStore errandPartStore = databaseServices.GetRequiredService<ErrandPartStore>();

            await LoadStores(suppliers, users, parts, errandPartStatuses, errands, errandTypes, errandPartStore);
            await CombineData(suppliers, users, parts, errandPartStatuses, errands, errandTypes, errandPartStore);
        }
        #region FETCH_DATA
        private async Task LoadStores(SuppliersStore suppliers, UserStore users, PartsStore parts, ErrandPartStatusStore errandPartStatuses, ErrandStore errands, ErrandTypeStore errandTypes, ErrandPartStore errandPartStore)
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
        private async Task CombineData(SuppliersStore suppliers, UserStore users, PartsStore parts, ErrandPartStatusStore statuses, ErrandStore errands, ErrandTypeStore errandTypes, ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(CombinePartWithSuppliers(parts, suppliers));
            tasks.Add(CombineStatusWithPerson(statuses, users));
            tasks.Add(CombineErrandWithTypePerson(errands, errandTypes, users));
            tasks.Add(CombineErrandWithParts(parts, errands, errandPartStore));
            await Task.WhenAll(tasks);
        }
        private static Task CombinePartWithSuppliers(PartsStore parts, SuppliersStore suppliers)
        {
            foreach (Part part in parts.Data)
            {
                part.SetSupplier(suppliers.Data.FirstOrDefault(supplier => supplier.Id == part.SupplierId));
                part.SetProducer(suppliers.Data.FirstOrDefault(producer => producer.Id == part.ProducerId));
            }
            return Task.CompletedTask;
        }
        private Task CombineStatusWithPerson(ErrandPartStatusStore statuses, UserStore users)
        {
            foreach (ErrandPartStatus status in statuses.Data)
            {
                status.CreatedBy = users.Data.FirstOrDefault(user => user.Id == status.CreatedById);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandWithParts(PartsStore parts, ErrandStore errands, ErrandPartStore errandParts)
        {
            _selectedRequest.Request!.Errand!.Parts.Clear();
            foreach (ErrandPart errandPart in errandParts.Data)
            {
                if (errandPart.ErrandId != _selectedRequest.Request!.ErrandId) continue;
                _selectedRequest.Request!.Errand!.Parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandWithTypePerson(ErrandStore errands, ErrandTypeStore types, UserStore users)
        {
            foreach (Errand errand in errands.Data)
            {
                errand.Type = types.Data.FirstOrDefault(type => type.Id == errand.TypeId);
                errand.CreatedByUser = users.Data.FirstOrDefault(user => user.Id == errand.CreatedById);
            }
            return Task.CompletedTask;
        }
        #endregion COMBINE_DATA
    }
}
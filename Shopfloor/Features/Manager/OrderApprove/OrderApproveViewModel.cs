using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.Commands;
using Shopfloor.Features.Manager.Stores;
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Manager.OrderApprove
{
    internal sealed partial class OrderApproveViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly SelectedRequestStore _requestStore;
        public ICommand ReturnCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ErrandPart ErrandPart => _requestStore.Request!;
        public string Comment { get; set; } = string.Empty;
        public DateTime? SelectedDate
        {
            get => ErrandPart.ExpectedDeliveryDate;
            set => ErrandPart.ExpectedDeliveryDate = value;
        }
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public OrderApproveViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            Task.Run(() => LoadData(databaseServices));
            _requestStore = _mainServices.GetRequiredService<SelectedRequestStore>();

            //ReturnCommand = new NavigateCommand<OrdersToApproveViewModel>(_mainServices.GetRequiredService<NavigationService<OrdersToApproveViewModel>>());
            ConfirmCommand = new ApproveOrderCommand(_requestStore, this, databaseServices, userServices, mainServices);

            _errandPartValidation = new(this);
        }

        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
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
            ErrandPart.Errand!.Parts.Clear();
            foreach (ErrandPart errandPart in errandParts.GetData)
            {
                if (errandPart.ErrandId != ErrandPart.ErrandId) continue;
                ErrandPart.Errand!.Parts.Add(errandPart);
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
            HistoricalData = errandParts.GetData.Where(part => part.PartId == ErrandPart.PartId);
        }
    }
    internal sealed partial class OrderApproveViewModel : IInputForm<ErrandPart>
    {
        private readonly ErrandPartValidation _errandPartValidation;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public bool IsDataValidate
        {
            get
            {
                return !HasErrors;
            }
        }
        public bool HasErrors => _propertyErrors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? value))
            {
                value = [];
                _propertyErrors.Add(propertyName, value);
            }
            value?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        public void CleanForm()
        {
            throw new NotImplementedException();
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                _propertyErrors.Clear();
                return;
            }
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}
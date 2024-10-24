using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Shopfloor.Features.Manager.Commands;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Manager.OrderApprove
{
    internal sealed partial class OrderApproveViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _requestStore;
        public OrderApproveViewModel(
            StoreRepository stores,
            SelectedRequestStore selectedRequestStore,
            INavigationService navigationService,
            INotifier notifier,
            SelectedRequestStore requestStore,
            IProvider<ErrandPartStatus> errandPartStatusProvider)
        {
            _requestStore = selectedRequestStore;

            ReturnCommand = new NavigationCommand<OrdersToApproveViewModel>(navigationService).Navigate();
            ConfirmCommand = new ApproveOrderCommand(navigationService, stores.ErrandPartStatus, notifier, requestStore, this, stores.CurrentUser, errandPartStatusProvider);

            _errandPartValidation = new(this);
        }
        public string Comment { get; set; } = string.Empty;
        public ICommand ConfirmCommand { get; }
        public ErrandPart ErrandPart => _requestStore.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public ICommand ReturnCommand { get; }
        public DateTime? SelectedDate
        {
            get => ErrandPart.ExpectedDeliveryDate;
            set => ErrandPart.ExpectedDeliveryDate = value;
        }
        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
        }
        private void LoadHistoricalData(IDataStore<ErrandPart> errandParts)
        {
            HistoricalData = errandParts.Data.Where(part => part.PartId == ErrandPart.PartId);
        }
    }
    internal sealed partial class OrderApproveViewModel : IInputForm<ErrandPart>
    {
        private readonly ErrandPartValidation _errandPartValidation;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate
        {
            get
            {
                return !HasErrors;
            }
        }
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
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}
using Shopfloor.Features.Manager.Commands;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ToastNotifications;

namespace Shopfloor.Features.Manager.OrderApprove
{
    internal sealed partial class OrderApproveViewModel : ViewModelBase
    {
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
        public OrderApproveViewModel(SelectedRequestStore selectedRequestStore, NavigationService navigationService, ErrandPartStatusStore errandPartStatusStore, Notifier notifier, SelectedRequestStore requestStore, ICurrentUserStore currentUserStore, ErrandPartStatusProvider errandPartStatusProvider)
        {
            _requestStore = selectedRequestStore;

            ReturnCommand = new RelayCommand(o => { navigationService.NavigateTo<OrdersToApproveViewModel>(); }, o => true);
            ConfirmCommand = new ApproveOrderCommand(navigationService, errandPartStatusStore, notifier, requestStore, this, currentUserStore, errandPartStatusProvider);

            _errandPartValidation = new(this);
        }

        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
        }
        private void LoadHistoricalData(ErrandPartStore errandParts)
        {
            HistoricalData = errandParts.Data.Where(part => part.PartId == ErrandPart.PartId);
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
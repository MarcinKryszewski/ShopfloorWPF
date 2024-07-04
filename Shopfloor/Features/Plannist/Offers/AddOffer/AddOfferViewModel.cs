using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Plannist.Offers.AddOffer;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;

namespace Shopfloor.Features.Plannist
{
    internal sealed partial class AddOfferViewModel : ViewModelBase
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _requestStore;
        public AddOfferViewModel(
            NavigationService navigationService,
            SelectedRequestStore selectedRequestStore,
            IDataStore<ErrandPart> errandPartStore,
            ICurrentUserStore currentUserStore,
            IProvider<ErrandPart> errandPartProvider,
            IProvider<ErrandPartStatus> errandPartStatusProvider,
            IDataStore<ErrandPartStatus> errandPartStatusStore,
            INotifier notifier)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;

            Task.Run(LoadData);

            ReturnCommand = new NavigationCommand<OffersViewModel>(navigationService).Navigate();
            ConfirmCommand = new ConfrmOfferCommand(_requestStore, this, currentUserStore, errandPartProvider, errandPartStatusProvider, errandPartStatusStore, notifier);

            _errandPartValidation = new(this);
        }
        public ICommand ConfirmCommand { get; }
        public ErrandPart ErrandPart => _requestStore.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public double PricePerUnit
        {
            get
            {
                if (ErrandPart is null)
                {
                    return 0;
                }

                if (ErrandPart.Amount is null)
                {
                    return 0;
                }

                return ErrandPart.PricePerUnit;
            }
            set
            {
                if (ErrandPart is null)
                {
                    return;
                }

                if (ErrandPart.Amount is null)
                {
                    return;
                }

                ErrandPart.SetPrice(value);
                OnPropertyChanged(nameof(PriceTotal));
                OnPropertyChanged(nameof(PricePerUnit));
            }
        }
        public double PriceTotal
        {
            get
            {
                if (ErrandPart is null)
                {
                    return 0;
                }

                if (ErrandPart.Amount is null)
                {
                    return 0;
                }

                return ErrandPart.PricePerUnit * (double)ErrandPart.Amount;
            }
            set
            {
                if (ErrandPart is null)
                {
                    return;
                }

                ErrandPart.SetPrice(value, ErrandPart.Amount);
                OnPropertyChanged(nameof(PriceTotal));
                OnPropertyChanged(nameof(PricePerUnit));
            }
        }
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
        private Task LoadData()
        {
            IDataStore<ErrandPart> errandPartStore = _errandPartStore;

            LoadHistoricalData(errandPartStore);
            return Task.CompletedTask;
        }
        private void LoadHistoricalData(IDataStore<ErrandPart> errandParts)
        {
            HistoricalData = errandParts.Data.Where(part => part.PartId == ErrandPart.PartId);
        }
    }
    internal sealed partial class AddOfferViewModel : IInputForm<ErrandPart>
    {
        private readonly ErrandPartValidation _errandPartValidation;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate
        {
            get
            {
                _errandPartValidation.ValidatePrice(nameof(PricePerUnit), PricePerUnit);
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OfferModel;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed partial class ErrandPartOffer
    {
        private readonly ErrandPartOfferDTO _data = new();
        public int? Id
        {
            get => _data.Id;
            set
            {
                if (_data.Id is not null)
                {
                    AddError(nameof(Id), "Id already assigned");
                    return;
                }
                _data.Id = value;
            }
        }
        public ErrandPart? ErrandPart
        {
            get => _data.ErrandPart;
            set
            {
                if (value is null)
                {
                    _data.ErrandPart = null;
                    return;
                }
                if (value.Id == ErrandPartId)
                {
                    _data.ErrandPart = value;
                    return;
                }
                AddError(nameof(ErrandPart), "ErrandPartId do not match");
            }
        }
        public required int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
        public Offer? Offer
        {
            get => _data.Offer;
            set
            {
                if (value is null)
                {
                    _data.Offer = null;
                    return;
                }
                if (value.Id == OfferId)
                {
                    _data.Offer = value;
                    return;
                }
                AddError(nameof(Offer), "OfferId do not match");
            }
        }
        public required int OfferId
        {
            get => _data.OfferId;
            init => _data.OfferId = value;
        }
    }
    internal sealed partial class ErrandPartOffer : INotifyDataErrorInfo
    {
        public bool HasErrors => _propertyErrors.Count != 0;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName) => _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        private void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
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
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
    }
}
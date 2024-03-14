using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.Order
{
    internal sealed partial class Order
    {
        private readonly OrderDTO _data = new();
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
        public required DateTime CreationDate
        {
            get => _data.CreationDate;
            init => _data.CreationDate = value;
        }
        public DateTime? DeliveryDate
        {
            get => _data.DeliveryDate;
            set => _data.DeliveryDate = value;
        }
        public bool Delivered
        {
            get => _data.Delivered;
            set => _data.Delivered = value;
        }
        public Order()
        {
            Delivered = false;
        }
    }
    internal sealed partial class Order : INotifyDataErrorInfo
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
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.PartModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed partial class ErrandPart
    {
        private readonly ErrandPartDTO _data = new();
        public ErrandPart(int errandId, int partId)
        {
            _data.ErrandId = errandId;
            _data.PartId = partId;
        }
        public ErrandPart(int errandId, int partId, double? amount, string? status = null)
        {
            _data.ErrandId = errandId;
            _data.PartId = partId;
            _data.Amount = amount;
            _data.Status = status ?? PartStatuses[0];
        }
        public int ErrandId => _data.ErrandId;
        public int PartId => _data.PartId;
        public Part? Part
        {
            get => _data.Part;
            set
            {
                if (value is null) return;
                if (value.Id == PartId) _data.Part = value;
            }
        }
        public string Status { get => _data.Status; set => _data.Status = value; }
        public double? Amount { get => _data.Amount; set => _data.Amount = value; }
        internal Errand? Errand
        {
            get => _data.Errand;
            set
            {
                if (value is null) return;
                if (value.Id == ErrandId) _data.Errand = value;
            }
        }
    }
    internal sealed partial class ErrandPart : INotifyDataErrorInfo
    {
        public bool HasErrors => _propertyErrors.Count != 0;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
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
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
    }
    internal sealed partial class ErrandPart
    {
        public static Dictionary<int, string> PartStatuses = new()
        {
            [0] = "ZATWIERDZANIE",
            [1] = "OFERTOWANIE",
            [2] = "ZAMAWIANIE",
            [3] = "DOSTARCZANIE",
            [4] = "POBIERANIE",
            [5] = "ZAKOŃCZONE",
            [6] = "ANULOWANE"
        };
    }
    internal sealed partial class ErrandPart : IEquatable<ErrandPart>
    {
        public bool Equals(ErrandPart? other)
        {
            if (other == null) return false;
            return ErrandId == other.ErrandId && PartId == other.PartId;
        }
        public override bool Equals(object? obj) => obj is ErrandPart objErrandPart && Equals(objErrandPart);
        public override int GetHashCode()
        {
            return ErrandId.GetHashCode() & PartId.GetHashCode();
        }
    }
}
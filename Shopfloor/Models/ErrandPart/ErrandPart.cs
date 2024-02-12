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
        private readonly int _errandId;
        private readonly int _partId;
        private Errand? _errand;
        private Part? _part;
        private double? _amount;
        private string _status = PartStatuses[0];
        public ErrandPart(int errandId, int partId)
        {
            _errandId = errandId;
            _partId = partId;
        }
        public ErrandPart(int errandId, int partId, double? amount, string? status = null)
        {
            _errandId = errandId;
            _partId = partId;
            _amount = amount;
            _status = status ?? PartStatuses[0];
        }
        public int ErrandId => _errandId;
        public int PartId => _partId;
        public Part? Part
        {
            get => _part;
            set
            {
                if (value is null) return;
                if (value.Id == _partId) _part = value;
            }
        }
        public string Status { get => _status; set => _status = value; }
        public double? Amount
        {
            get => _amount;
            set => _amount = value;
        }
        internal Errand? Errand
        {
            get => _errand;
            set
            {
                if (value is null) return;
                if (value.Id == _errandId) _errand = value;
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
            return _errandId == other.ErrandId && _partId == other.PartId;
        }
        public override bool Equals(object? obj) => obj is ErrandPart objErrandPart && Equals(objErrandPart);
        public override int GetHashCode()
        {
            return _errandId.GetHashCode() & _partId.GetHashCode();
        }
    }
}
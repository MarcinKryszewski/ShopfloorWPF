using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.PartModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        public ErrandPart(int errandId, int partId, double? amount)
        {
            _data.ErrandId = errandId;
            _data.PartId = partId;
            _data.Amount = amount;
        }
        public ErrandPart(int id, int errandId, int partId, double? amount)
        {
            _data.Id = id;
            _data.ErrandId = errandId;
            _data.PartId = partId;
            _data.Amount = amount;
        }
        public int ErrandId => _data.ErrandId;
        public int PartId => _data.PartId;
        public int? Id => _data.Id;
        public Part? Part
        {
            get => _data.Part;
            set
            {
                if (value is null) return;
                if (value.Id == PartId) _data.Part = value;
            }
        }
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
        public List<ErrandPartStatus> StatusList = [];
        private ErrandPartStatus _lastStatus => StatusList.Where(status => status.StatusValue >= 0)
                                                .OrderBy(status => status.CreatedDate)
                                                .First();
        public int LastStatusValue
        {
            get
            {
                if (StatusList.Count > 0) return _lastStatus.StatusValue;
                return -1;
            }
        }
        public string LastStatusUpdateDate
        {
            get
            {
                if (StatusList.Count > 0) return _lastStatus.CreatedDate.ToString();
                return "NIGDY";
            }
        }
        public string LastStatusText => LastStatusValue == -1 ? "ERROR" : ErrandPartStatus.Status[LastStatusValue];
    }
    internal sealed partial class ErrandPart : INotifyDataErrorInfo
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
    internal sealed partial class ErrandPart : IEquatable<ErrandPart>
    {
        public bool Equals(ErrandPart? other)
        {
            if (other == null) return false;
            return ErrandId == other.ErrandId && PartId == other.PartId;
        }
        public override bool Equals(object? obj) => obj is ErrandPart objErrandPart && Equals(objErrandPart);
        public override int GetHashCode() => ErrandId.GetHashCode() & PartId.GetHashCode();
    }
}
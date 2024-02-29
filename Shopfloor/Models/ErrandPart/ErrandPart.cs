using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
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
        public ErrandPart(int errandId, int partId, double? amount, int orderedBy)
        {
            _data.ErrandId = errandId;
            _data.PartId = partId;
            _data.Amount = amount;
            _data.OrderedById = orderedBy;
        }
        public ErrandPart(int id, int errandId, int partId, double? amount, int orderedBy)
        {
            _data.Id = id;
            _data.ErrandId = errandId;
            _data.PartId = partId;
            _data.Amount = amount;
            _data.OrderedById = orderedBy;
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
        public string AmountText => Amount + ((Part is not null) ? " " + Part.Unit : "");
        public Errand? Errand
        {
            get => _data.Errand;
            set
            {
                if (value is null) return;
                if (value.Id == ErrandId) _data.Errand = value;
            }
        }
        public List<ErrandPartStatus> StatusList = [];
        public IEnumerable<ErrandPartStatus> StatusListDisplay => StatusList;
        private ErrandPartStatus LastStatus => StatusList.Where(status => status.StatusValue >= 0)
                                                .OrderByDescending(status => status.CreatedDate)
                                                .First();
        public int LastStatusValue => StatusList.Count > 0 ? LastStatus.StatusValue : -1;
        public string LastStatusUpdateDate => StatusList.Count > 0 ? LastStatus.CreatedDate.ToString("dd/MM/yyyy") : "NIGDY";
        public string LastStatusText => LastStatusValue == -1 ? "ERROR" : ErrandPartStatus.Status[LastStatusValue];
        public int OrderedById => _data.OrderedById;
        public User? OrderedByUser
        {
            get => _data.OrderedByUser;
            set
            {
                if (value is null) return;
                if (value.Id == OrderedById) _data.OrderedByUser = value;
            }
        }
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
    internal sealed partial class ErrandPart : ISearchableModel
    {
        public string SearchValue => LastStatusText;
    }
}
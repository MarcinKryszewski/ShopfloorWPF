using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed partial class ErrandPartStatus
    {
        private readonly ErrandPartStatusDTO _data = new();
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
        public required int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
        public string StatusName => _data.StatusName;
        public int? CompletedById
        {
            get => _data.CompletedById;
            set => _data.CompletedById = value;
        }
        public required DateTime CreatedDate
        {
            get => _data.CreatedDate;
            init => _data.CreatedDate = value;
        }
        public string CreatedDateDisplay => _data.CreatedDate.ToShortDateString();
        public string? Comment
        {
            get => _data.Comment;
            set => _data.Comment = value;
        }
        public string? Reason
        {
            get => _data.Reason;
            init => _data.Reason = value ?? "SYSTEM";
        }
        public int StatusValue;
        public bool Confirmed
        {
            get => _data.Confirmed;
            init => _data.Confirmed = value;
        }
        public User? CompletedBy
        {
            get => _data.CompletedBy;
            set => _data.CompletedBy = value;
        }
        public ErrandPartStatus() { }
        public ErrandPartStatus(int statusId)
        {
            SetStatus(statusId);
        }
        public ErrandPartStatus(string statusName)
        {
            SetStatus(statusName);
        }
        public void SetStatus(int id)
        {
            _data.StatusName = Status[id];
            StatusValue = id;
        }
        public void Confirm() => _data.Confirmed = true;
        public void Abort() => _data.Confirmed = false;
        public void SetStatus(string name) => SetStatus(Status.FirstOrDefault(x => x.Value == name).Key);
        public static Dictionary<int, string> Status = new()
        {
            [-1] = "ERROR",
            [0] = "OFERTOWANIE",
            [1] = "ZATWIERDZANIE",
            [2] = "KOREKCJA",
            [3] = "ZAMAWIANIE",
            [4] = "DOSTARCZANIE",
            [5] = "REZERWOWANIE",
            [6] = "POBIERANIE",
            [7] = "ZAKOŃCZONE",
            [8] = "WSTRZYMANE",
            [9] = "ANULOWANE",
        };
    }
    internal sealed partial class ErrandPartStatus : ISearchableModel
    {
        public string SearchValue => throw new NotImplementedException();
    }
    internal sealed partial class ErrandPartStatus : INotifyDataErrorInfo
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
using System;
using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed partial class ErrandPartStatus : DataModel
    {
        public static readonly Dictionary<int, string> Status = new()
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
        private readonly ErrandPartStatusDto _data;

        public ErrandPartStatus()
        {
            _data = new();
        }
        public ErrandPartStatus(int statusId)
        {
            _data = new();
            SetStatus(statusId);
        }
        public ErrandPartStatus(string statusName)
        {
            _data = new();
            SetStatus(statusName);
        }
        public string? Comment
        {
            get => _data.Comment;
            set => _data.Comment = value;
        }
        public User? CompletedBy
        {
            get => _data.CompletedBy;
            set => _data.CompletedBy = value;
        }
        public int? CompletedById
        {
            get => _data.CompletedById;
            set => _data.CompletedById = value;
        }
        public bool Confirmed
        {
            get => _data.Confirmed;
            init => _data.Confirmed = value;
        }
        required public DateTime CreatedDate
        {
            get => _data.CreatedDate;
            init => _data.CreatedDate = value;
        }
        public string CreatedDateDisplay => _data.CreatedDate.ToShortDateString();
        required public int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
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
        public string? Reason
        {
            get => _data.Reason;
            init => _data.Reason = value ?? "SYSTEM";
        }
        public string StatusName => _data.StatusName;

        public int StatusValue { get; set; }

        public void Abort() => _data.Confirmed = false;
        public void Confirm() => _data.Confirmed = true;
        public void SetStatus(int id)
        {
            _data.StatusName = Status[id];
            StatusValue = id;
        }
        public void SetStatus(string name) => SetStatus(Status.FirstOrDefault(x => x.Value == name).Key);
    }
    internal sealed partial class ErrandPartStatus : ISearchableModel
    {
        public string SearchValue => throw new NotImplementedException();
    }
}
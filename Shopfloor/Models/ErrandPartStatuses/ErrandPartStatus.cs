using System;
using System.Collections.Generic;
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
            init => _data.Id = value ?? 0;
        }
        public required int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
        public string StatusName => _data.StatusName;
        public required int CreatedById
        {
            get => _data.CreatedById;
            init => _data.CreatedById = value;
        }
        public required DateTime CreatedDate
        {
            get => _data.CreatedDate;
            init => _data.CreatedDate = value;
        }
        public string CreatedDateDisplay => _data.CreatedDate.ToShortDateString();
        public string? Comment
        {
            get => _data.Comment ?? "";
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
        public User? CreatedBy
        {
            get => _data.CreatedBy;
            set => _data.CreatedBy = value;
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
        public ErrandPartStatus(int errandPartId, int createdById, int statusId, DateTime createdDate, string? comment = null, string? reason = "SYSTEM", bool confirmed = false)
        {
            _data.ErrandPartId = errandPartId;
            _data.CreatedById = createdById;
            _data.CreatedDate = createdDate;
            _data.Comment = comment;
            _data.Reason = reason;
            _data.Confirmed = confirmed;
            SetStatus(statusId);
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
            [2] = "ZAMAWIANIE",
            [3] = "DOSTARCZANIE",
            [4] = "POBIERANIE",
            [5] = "ZAKOŃCZONE",
            [-6] = "ANULOWANE",
            [-7] = "AKTUALIZACJA"
        };
    }
    internal sealed partial class ErrandPartStatus : ISearchableModel
    {
        public string SearchValue => throw new NotImplementedException();
    }
}
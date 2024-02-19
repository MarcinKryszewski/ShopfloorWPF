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
        public int Id => _data.Id;
        public int ErrandPartId => _data.ErrandPartId;
        public string StatusName => _data.StatusName;
        public int CreatedById => _data.CreatedById;
        public DateTime CreatedDate => _data.CreatedDate;
        public string CreatedDateDisplay => _data.CreatedDate.ToShortDateString();
        public string? Comment => _data.Comment;
        public string? Reason => _data.Reason;
        public int StatusValue;
        public User? CreatedBy
        {
            get => _data.CreatedBy;
            set => _data.CreatedBy = value;
        }
        public ErrandPartStatus(int id, int errandPartId, string statusName, DateTime createdDate, int createdById, string? comment = null, string? reason = "SYSTEM")
        {
            _data.Id = id;
            _data.ErrandPartId = errandPartId;
            _data.StatusName = statusName;
            _data.CreatedDate = createdDate;
            _data.CreatedById = createdById;
            _data.Comment = comment;
            _data.Reason = reason;
            SetStatus(statusName);
        }
        public ErrandPartStatus(int errandPartId, int createdById, int statusId, DateTime createdDate, string? comment = null, string? reason = "SYSTEM")
        {
            _data.ErrandPartId = errandPartId;
            _data.CreatedById = createdById;
            _data.CreatedDate = createdDate;
            _data.Comment = comment;
            _data.Reason = reason;
            SetStatus(statusId);
        }
        public ErrandPartStatus(int errandPartId, int createdById, DateTime createdDate, string? comment = null, string? reason = "SYSTEM")
        {
            _data.ErrandPartId = errandPartId;
            _data.CreatedById = createdById;
            _data.CreatedDate = createdDate;
            _data.Comment = comment;
            _data.Reason = reason;
        }
        public void SetStatus(int id)
        {
            _data.StatusName = Status[id];
            StatusValue = id;
        }
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
}
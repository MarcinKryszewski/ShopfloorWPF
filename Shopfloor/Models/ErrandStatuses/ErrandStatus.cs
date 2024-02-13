using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus
    {
        private readonly ErrandStatusDTO _data = new();

        public ErrandStatus(string statusName, string? comment, string? reason, DateTime? setDate)
        {
            _data.StatusName = statusName;
            _data.SetDate = setDate ?? DateTime.Now;
            _data.Comment = comment ?? string.Empty;
            _data.Reason = reason ?? string.Empty;
        }

        public ErrandStatus(int errandId, string statusName, DateTime? setDate)
        {
            _data.ErrandId = errandId;
            _data.StatusName = statusName;
            _data.SetDate = setDate ?? DateTime.Now;
        }

        public ErrandStatus(int id, int errandId, string statusName, DateTime setDate, string comment, string reason)
        {
            _data.Id = id;
            _data.ErrandId = errandId;
            _data.StatusName = statusName;
            _data.SetDate = setDate;
            _data.Comment = comment;
            _data.Reason = reason;
        }

        public int Id => _data.Id;
        public int ErrandId => _data.ErrandId;
        public string StatusName
        {
            get => _data.StatusName;
            set => _data.StatusName = value;
        }
        public DateTime SetDate => _data.SetDate;
        public string Comment
        {
            get => _data.Comment;
            set => _data.Comment = value;
        }
        public string Reason
        {
            get => _data.Reason;
            set => _data.Reason = value;
        }
    }
}
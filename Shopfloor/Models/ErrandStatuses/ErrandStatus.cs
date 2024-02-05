using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus
    {
        private readonly int _id;
        private readonly int _errandId;
        private string _statusName;
        private readonly DateTime _setDate;
        private string _comment = string.Empty;
        private string _reason = string.Empty;

        public ErrandStatus(string statusName, string? comment, string? reason, DateTime? setDate)
        {
            _statusName = statusName;
            _setDate = setDate ?? DateTime.Now;
            _comment = comment ?? string.Empty;
            _reason = reason ?? string.Empty;
        }

        public ErrandStatus(int errandId, string statusName, DateTime? setDate)
        {
            _errandId = errandId;
            _statusName = statusName;
            _setDate = setDate ?? DateTime.Now;
        }

        public ErrandStatus(int id, int errandId, string statusName, DateTime setDate, string comment, string reason)
        {
            _id = id;
            _errandId = errandId;
            _statusName = statusName;
            _setDate = setDate;
            _comment = comment;
            _reason = reason;
        }

        public int Id => _id;
        public int ErrandId => _errandId;
        public string StatusName
        {
            get => _statusName;
            set => _statusName = value;
        }
        public DateTime SetDate => _setDate;
        public string Comment
        {
            get => _comment;
            set => _comment = value;
        }
        public string Reason
        {
            get => _reason;
            set => _reason = value;
        }


    }
}
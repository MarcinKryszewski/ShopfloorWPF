using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus
    {
        private readonly ErrandStatusDTO _data = new();
        public ErrandStatus() { }
        public int Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        public required int ErrandId
        {
            get => _data.ErrandId;
            init => _data.ErrandId = value;
        }
        public required string StatusName
        {
            get => _data.StatusName;
            set => _data.StatusName = value;
        }
        public required DateTime SetDate
        {
            get => _data.SetDate;
            init => _data.SetDate = value;
        }
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
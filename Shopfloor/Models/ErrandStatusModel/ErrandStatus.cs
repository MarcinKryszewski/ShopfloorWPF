using Shopfloor.Shared.BaseClasses;
using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus : DataModel
    {
        private readonly ErrandStatusDTO _data;
        private const string _existingIdErrorMassage = "Id already exists";
        public ErrandStatus(int id = 0)
        {
            _data = new()
            {
                Id = id
            };
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                string myName = nameof(Id);
                ClearErrors(myName);

                if (value == null || _data.Id == 0) _data.Id = value;

                AddError(myName, _existingIdErrorMassage);
            }
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
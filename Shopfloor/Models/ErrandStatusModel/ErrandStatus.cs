using System;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus : DataModel
    {
        private const string _existingIdErrorMassage = "Id already exists";
        private readonly ErrandStatusDto _data;
        public ErrandStatus(int id = 0)
        {
            _data = new()
            {
                Id = id,
            };
        }
        public string Comment
        {
            get => _data.Comment;
            set => _data.Comment = value;
        }
        required public int ErrandId
        {
            get => _data.ErrandId;
            init => _data.ErrandId = value;
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                string myName = nameof(Id);
                ClearErrors(myName);

                if (value == null || _data.Id == 0)
                {
                    _data.Id = value;
                }

                AddError(myName, _existingIdErrorMassage);
            }
        }
        public string Reason
        {
            get => _data.Reason;
            set => _data.Reason = value;
        }
        required public DateTime SetDate
        {
            get => _data.SetDate;
            init => _data.SetDate = value;
        }
        required public string StatusName
        {
            get => _data.StatusName;
            set => _data.StatusName = value;
        }
    }
}
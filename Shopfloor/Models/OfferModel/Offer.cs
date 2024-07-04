using System;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.OfferModel
{
    internal sealed partial class Offer : DataModel
    {
        private readonly OfferDto _data;
        public Offer()
        {
            _data = new();
        }
        required public int CreateById
        {
            get => _data.CreateById;
            init => _data.CreateById = value;
        }
        required public DateTime CreateDate
        {
            get => _data.CreateDate;
            init => _data.CreateDate = value;
        }
        public User? CreatedBy
        {
            get => _data.CreateBy;
            set => _data.CreateBy = value;
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
    }
}
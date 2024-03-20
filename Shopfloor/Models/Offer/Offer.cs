using System;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.OfferModel
{
    internal sealed partial class Offer : DataModel
    {
        private readonly OfferDTO _data;
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
        public required DateTime CreateDate
        {
            get => _data.CreateDate;
            init => _data.CreateDate = value;
        }
        public required int CreateById
        {
            get => _data.CreateById;
            init => _data.CreateById = value;
        }
        public User? CreatedBy
        {
            get => _data.CreateBy;
            set => _data.CreateBy = value;
        }
        public Offer()
        {
            _data = new();
        }
    }
}
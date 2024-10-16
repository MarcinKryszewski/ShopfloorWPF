using System;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.OrderModel
{
    internal sealed partial class Order : DataModel
    {
        private readonly OrderDto _data;
        public Order()
        {
            _data = new();
            Delivered = false;
        }
        required public DateTime CreationDate
        {
            get => _data.CreationDate;
            init => _data.CreationDate = value;
        }
        public bool Delivered
        {
            get => _data.Delivered;
            set => _data.Delivered = value;
        }
        public DateTime? DeliveryDate
        {
            get => _data.DeliveryDate;
            set => _data.DeliveryDate = value;
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
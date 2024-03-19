using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.OrderModel
{
    internal sealed partial class Order : DataModel
    {
        private readonly OrderDTO _data = new();
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
        public required DateTime CreationDate
        {
            get => _data.CreationDate;
            init => _data.CreationDate = value;
        }
        public DateTime? DeliveryDate
        {
            get => _data.DeliveryDate;
            set => _data.DeliveryDate = value;
        }
        public bool Delivered
        {
            get => _data.Delivered;
            set => _data.Delivered = value;
        }
        public Order()
        {
            Delivered = false;
        }
    }
}
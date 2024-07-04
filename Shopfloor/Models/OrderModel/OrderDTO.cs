using System;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderDto
    {
        public DateTime CreationDate { get; set; }
        public bool Delivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? Id { get; set; }
    }
}
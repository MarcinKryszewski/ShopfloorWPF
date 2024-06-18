using System;

namespace Shopfloor.Models.Order
{
    internal sealed class OrderDTO
    {
        public int? Id { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Delivered { get; set; }
    }
}
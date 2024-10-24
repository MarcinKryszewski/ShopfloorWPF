using System;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartDto
    {
        public int? Id { get; set; }
        public int ErrandId { get; set; }
        public int PartId { get; set; }
        public double? Amount { get; set; }
        public int OrderedById { get; set; }
        public Part? Part { get; set; }
        public Errand? Errand { get; set; }
        public User? OrderedByUser { get; set; }
        public double PricePerUnit { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public bool Canceled { get; set; }
    }
}
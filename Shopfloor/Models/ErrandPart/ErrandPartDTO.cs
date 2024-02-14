using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartDTO
    {
        public int Id { get; set; }
        public int ErrandId { get; set; }
        public int PartId { get; set; }
        public double? Amount { get; set; }
        public Part? Part { get; set; }
        public Errand? Errand { get; set; }
    }
}
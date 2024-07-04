using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OrderModel;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderDto
    {
        public ErrandPart? ErrandPart { get; set; }
        public int ErrandPartId { get; set; }
        public int? Id { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
    }
}
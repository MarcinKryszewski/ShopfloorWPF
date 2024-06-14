using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OrderModel;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderDTO
    {
        public int? Id { get; set; }
        public ErrandPart? ErrandPart { get; set; }
        public int ErrandPartId { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
    }
}
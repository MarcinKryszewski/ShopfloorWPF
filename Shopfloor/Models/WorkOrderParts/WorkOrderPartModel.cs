using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Models.WorkOrderParts
{
    internal class WorkOrderPartModel : IModel
    {
        required public int Id { get; init; }
        required public int PartId { get; init; }
        required public int WorkOrderId { get; init; }
        public PartModel? Part { get; set; }
        public WorkOrderModel? WorkOrder { get; set; }
        public double Amount { get; set; } = 0;
        public double ValuePerPiece { get; set; } = 0;
        public double TotalValue => ValuePerPiece * Amount;
        public string Unit => Part?.Unit ?? "szt.";
    }
}
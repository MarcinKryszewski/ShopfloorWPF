using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Models.WorkOrderParts
{
    internal class WorkOrderPartCreationModel : ModelValidationBase, IModelCreationModel<WorkOrderPartModel>
    {
        public int? Id { get; set; }
        public int PartId { get; set; }
        public int WorkOrderId { get; set; }
        public double Amount { get; set; } = 0;
        public double ValuePerPiece { get; set; } = 0;
        public double TotalValue => ValuePerPiece * Amount;
        public string Unit => Part?.Unit ?? "szt.";
        public PartModel? Part { get; set; }
        public WorkOrderModel? WorkOrder { get; set; }
        public WorkOrderPartModel CreateModel(int id)
        {
            return new()
            {
                Id = id,
                PartId = PartId,
                WorkOrderId = WorkOrderId,
                Amount = Amount,
                ValuePerPiece = ValuePerPiece,
            };
        }
        public WorkOrderPartCreationModel CreateFromModel(WorkOrderPartModel model)
        {
            Id = model.Id;
            PartId = model.PartId;
            WorkOrderId = model.WorkOrderId;
            Amount = model.Amount;
            ValuePerPiece = model.ValuePerPiece;
            Part = model.Part;
            WorkOrder = model.WorkOrder;

            return this;
        }
    }
}
using System.Collections.Generic;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderModel
    {
        required public int Id { get; init; }
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; init; } = [];
    }
}
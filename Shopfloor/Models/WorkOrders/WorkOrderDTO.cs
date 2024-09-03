using System.Collections.Generic;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderDto
    {
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; set; } = [];
    }
}
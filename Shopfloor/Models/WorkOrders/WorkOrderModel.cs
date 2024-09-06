using System.Collections.Generic;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderModel : IModel
    {
        required public int Id { get; init; }
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; init; } = [];
        required public LineModel Line { get; init; }
    }
}
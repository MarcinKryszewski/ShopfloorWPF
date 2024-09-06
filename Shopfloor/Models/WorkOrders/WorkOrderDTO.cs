using System.Collections.Generic;
using Shopfloor.Models.Interfaces;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderDto : IModelDto
    {
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; set; } = [];
        public LineModel? Line { get; set; }
    }
}
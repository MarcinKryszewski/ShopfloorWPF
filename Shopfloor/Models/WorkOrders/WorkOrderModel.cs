using System;
using System.Collections.Generic;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderModel : IModel
    {
        required public int Id { get; init; }
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; init; } = [];
        public List<int> PartsId { get; init; } = [];
        public LineModel? Line { get; init; }
        required public int LineId { get; init; }
        public DateTime CreateDate { get; init; }
        public DateOnly CreateDateOnlyDate => DateOnly.FromDateTime(CreateDate);
    }
}
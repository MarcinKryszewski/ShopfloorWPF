using System.Collections.Generic;

namespace Shopfloor.Models
{
    public class WorkOrderModel
    {
        public int Id { get; set; }
        public string Descritpion { get; set; } = string.Empty;
        public List<PartModel> Parts { get; set; } = [];
    }
}
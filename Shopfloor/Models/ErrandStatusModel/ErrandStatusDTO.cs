using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusDTO
    {
        public int? Id { get; set; }
        public int ErrandId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime SetDate { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
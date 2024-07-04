using System;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusDto
    {
        public string Comment { get; set; } = string.Empty;
        public int ErrandId { get; set; }
        public int? Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime SetDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
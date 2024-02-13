using System;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusDTO
    {
        public int Id { get; set; }
        public int ErrandPartId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public string? Comment { get; set; }
        public string? Reason { get; set; }

    }
}
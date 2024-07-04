using System;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusDto
    {
        public string? Comment { get; set; }
        public User? CompletedBy { get; set; }
        public int? CompletedById { get; set; }
        public bool Confirmed { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ErrandPartId { get; set; }
        public int? Id { get; set; }
        public string? Reason { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
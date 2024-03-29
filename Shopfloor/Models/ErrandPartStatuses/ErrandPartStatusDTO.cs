using System;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusDTO
    {
        public int? Id { get; set; }
        public int ErrandPartId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int? CompletedById { get; set; }
        public string? Comment { get; set; }
        public string? Reason { get; set; }
        public User? CompletedBy { get; set; }
        public bool Confirmed { get; set; }
    }
}
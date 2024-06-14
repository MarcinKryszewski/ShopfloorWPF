using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using System;

namespace Shopfloor.Models.ErrandModel
{
    internal class ErrandDTO
    {
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public int? ErrandTypeId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public int? Id { get; set; }
        public int? MachineId { get; set; }
        public int? OwnerId { get; set; }
        public string? Priority { get; set; }
        public string? SapNumber { get; set; }
        public ErrandType? ErrandType { get; set; }
        public Machine? Machine { get; set; }
        public User? Responsible { get; set; }
    }
}
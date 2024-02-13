namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Number { get; set; } = string.Empty;
        public string? SapNumber { get; set; } = string.Empty;
        public int? Parent { get; set; }
        public bool Active { get; set; }
    }
}
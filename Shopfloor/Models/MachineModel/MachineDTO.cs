namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineDto
    {
        public bool Active { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Number { get; set; } = string.Empty;
        public int? Parent { get; set; }
        public string? SapNumber { get; set; } = string.Empty;
    }
}
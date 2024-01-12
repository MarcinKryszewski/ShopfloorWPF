namespace Shopfloor.Database.DTOs
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public int Parent { get; set; } = 0;
    }
}
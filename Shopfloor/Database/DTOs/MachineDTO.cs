namespace Shopfloor.Database.DTOs
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Machine_Name { get; set; } = string.Empty;
        public string Machine_Number { get; set; } = string.Empty;
        public int? Parent { get; set; }
        public bool Active { get; set; }
    }
}
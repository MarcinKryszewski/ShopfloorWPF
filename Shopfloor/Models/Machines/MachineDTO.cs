namespace Shopfloor.Models.MachineModel
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Machine_Name { get; set; } = string.Empty;
        public string Machine_Number { get; set; } = string.Empty;
        public string Sap_Number { get; set; } = string.Empty;
        public int? Parent { get; set; }
        public bool Active { get; set; }
    }
}
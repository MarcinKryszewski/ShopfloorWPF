namespace Shopfloor.Models.PartErrandModel
{
    public class PartErrandDTO
    {
        public int Part_Id { get; set; }
        public int Errand_Id { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
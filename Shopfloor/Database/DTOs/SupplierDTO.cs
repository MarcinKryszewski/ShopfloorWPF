namespace Shopfloor.Database.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
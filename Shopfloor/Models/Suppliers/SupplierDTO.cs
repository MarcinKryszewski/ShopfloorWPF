namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SupplierDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
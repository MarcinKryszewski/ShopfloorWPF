namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SupplierDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
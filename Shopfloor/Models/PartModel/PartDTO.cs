using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartDto
    {
        public string? Details { get; set; }
        public int? Id { get; set; }
        public int? Index { get; set; }
        public string NameOriginal { get; set; } = string.Empty;
        public string? NamePl { get; set; }
        public string Number { get; set; } = string.Empty;
        public PartType? PartType { get; set; }
        public Supplier? Producer { get; set; }
        public int ProducerId { get; set; }
        public double StorageAmount { get; set; }
        public double StorageValue { get; set; }
        public Supplier? Supplier { get; set; }
        public int? SupplierId { get; set; }
        public int TypeId { get; set; }
        public string Unit { get; set; } = GlobalConstants.DefaultPartUnit;
    }
}
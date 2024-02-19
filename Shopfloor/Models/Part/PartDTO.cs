namespace Shopfloor.Models.PartModel
{
    internal sealed class PartDTO
    {
        public int? Id { get; set; }
        public string NamePl { get; set; } = string.Empty;
        public string NameOriginal { get; set; } = string.Empty;
        public int? TypeId { get; set; }
        public int? Index { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int? ProducerId { get; set; }
        public int? SupplierId { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
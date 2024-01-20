namespace Shopfloor.Database.DTOs
{
    public class PartDTO
    {
        public int Id { get; set; }
        public string Name_Pl { get; set; } = string.Empty;
        public string Name_Original { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Indeks { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int Producer { get; set; }
        public int Supplier { get; set; }
    }
}
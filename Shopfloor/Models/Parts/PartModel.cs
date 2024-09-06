namespace Shopfloor.Models
{
    public class PartModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
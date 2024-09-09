namespace Shopfloor.Models
{
    public class PartModel : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = "szt.";
    }
}
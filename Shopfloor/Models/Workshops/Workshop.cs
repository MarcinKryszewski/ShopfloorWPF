using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class Workshop : IModel
    {
        required public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineDto : IModelDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
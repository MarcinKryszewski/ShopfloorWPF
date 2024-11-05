using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineCreation : ModelValidationBase, IModelCreationModel<Line>
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Line CreateModel(int id)
        {
            return new Line() { Id = id, Name = Name };
        }
    }
}
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.H1Sheets
{
    internal class H1SheetCreation : ModelValidationBase, IModelCreationModel<H1Sheet>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public H1Sheet CreateModel(int id)
        {
            return new H1Sheet()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SafetySheets
{
    internal class SafetySheetCreation : ModelValidationBase, IModelCreationModel<SafetySheet>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SafetySheet CreateModel(int id)
        {
            return new SafetySheet()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class WorkshopCreation : ModelValidationBase, IModelCreationModel<Workshop>
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Workshop CreateModel(int id)
        {
            return new Workshop()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
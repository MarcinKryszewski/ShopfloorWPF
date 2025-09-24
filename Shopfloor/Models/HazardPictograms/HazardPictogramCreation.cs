using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardPictograms
{
    internal class HazardPictogramCreation : ModelValidationBase, IModelCreationModel<HazardPictogram>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public HazardPictogram CreateModel(int id)
        {
            return new HazardPictogram()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
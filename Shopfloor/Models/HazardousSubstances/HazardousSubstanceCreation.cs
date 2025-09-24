using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardousSubstances
{
    internal class HazardousSubstanceCreation : ModelValidationBase, IModelCreationModel<HazardousSubstance>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Nds { get; init; }
        public double Ndsch { get; init; }
        public HazardousSubstance CreateModel(int id)
        {
            return new HazardousSubstance()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
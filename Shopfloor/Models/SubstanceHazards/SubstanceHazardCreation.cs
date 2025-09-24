using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SubstanceHazards
{
    internal class SubstanceHazardCreation : ModelValidationBase, IModelCreationModel<SubstanceHazard>
    {
        public int Id { get; init; }
        public int SubstanceId { get; init; }
        public int HazardId { get; init; }
        public string Name { get; set; } = string.Empty;
        public SubstanceHazard CreateModel(int id)
        {
            return new SubstanceHazard()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}
using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Substances
{
    internal class SubstanceCreation : ModelValidationBase, IModelCreationModel<Substance>
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProducerName { get; set; } = string.Empty;
        public string SubstanceType { get; set; } = string.Empty;
        public double BoilingPoint { get; set; }
        public bool IsActive { get; set; }
        public bool IsFoodSafe { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Substance CreateModel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Substances
{
    internal class Substance : IModel
    {
        public int Id { get; init; }
        public string ProducerName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SubstanceType { get; set; } = string.Empty;
        public double BoilingPoint { get; set; }
        public bool IsActive { get; set; }
        public bool IsFoodSafe { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}
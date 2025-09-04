using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Substances;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.WorkshopSubstances
{
    internal class WorkshopSubstanceCreation : ModelValidationBase, IModelCreationModel<WorkshopSubstance>
    {
        public int Id { get; set; }
        public string Name { get; } = string.Empty;
        public Workshop? Workshop { get; set; }
        public int WorkshopId { get; set; }
        public Substance? Substance { get; set; }
        public int SubstanceId { get; set; }
        public WorkshopSubstance CreateModel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
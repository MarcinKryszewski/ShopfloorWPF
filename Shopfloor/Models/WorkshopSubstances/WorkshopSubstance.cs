using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Substances;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.WorkshopSubstances
{
    internal class WorkshopSubstance : IModel
    {
        required public int Id { get; init; }
        public string Name { get; } = string.Empty;
        public Workshop? Workshop { get; set; }
        required public int WorkshopId { get; init; }
        public Substance? Substance { get; set; }
        required public int SubstanceId { get; init; }
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            throw new System.NotImplementedException();
        }
    }
}
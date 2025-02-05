using Shopfloor.Models.Workshops;

namespace Shopfloor.Features.Actions.ActionTransfer.Utilities.Filters
{
    internal class FilterWorkshop
    {
        public Workshop? Workshop { get; set; }
        public bool Filter(object obj)
        {
            if (obj is ResponsibleTraining training && Workshop is not null)
            {
                return training.Responsible.WorkshopId == Workshop.Id;
            }
            return false;
        }
    }
}
using Shopfloor.Features.Mechanic.Errands.ErrandPartsList;

namespace Shopfloor.Features.Mechanic.Errands.Interfaces
{
    internal interface IPartsList
    {
        public ErrandPartsListViewModel? PartsList { get; set; }
    }
}
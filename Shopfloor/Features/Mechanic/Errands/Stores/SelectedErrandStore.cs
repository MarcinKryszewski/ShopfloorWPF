using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using System.Collections.ObjectModel;

namespace Shopfloor.Features.Mechanic.Errands.Stores
{
    internal sealed class SelectedErrandStore
    {
        public int? ErrandId { get; set; }
        public int? MachineId { get; set; }
        public Errand? SelectedErrand { get; set; }
        public ObservableCollection<ErrandPart> ErrandParts = [];
    }
}
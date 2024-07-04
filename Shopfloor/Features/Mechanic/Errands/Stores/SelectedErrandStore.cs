using System.Collections.ObjectModel;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Mechanic.Errands.Stores
{
    internal sealed class SelectedErrandStore
    {
        private Errand? _selectedErrand;
        public int? ErrandId { get; set; }
        public ObservableCollection<ErrandPart> ErrandParts { get; } = [];
        public int? MachineId { get; set; }
        public Errand? SelectedErrand
        {
            get => _selectedErrand;
            set
            {
                _selectedErrand = value;
                if (value is not null)
                {
                    ErrandId = value.Id;
                    MachineId = value.MachineId;
                }
            }
        }
    }
}
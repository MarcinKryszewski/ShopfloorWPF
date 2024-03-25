using Shopfloor.Models.MachinePartModel.Store.Combine;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartStore : StoreBase<MachinePart>
    {
        public MachinePartStore(MachinePartProvider provider, MachinePartCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
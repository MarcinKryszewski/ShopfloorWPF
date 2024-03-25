using Shopfloor.Models.MachineModel.Store;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineStore : StoreBase<Machine>
    {
        public MachineStore(MachineProvider provider, MachineCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
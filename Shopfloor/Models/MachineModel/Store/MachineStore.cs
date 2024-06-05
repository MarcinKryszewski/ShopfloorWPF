using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineStore : StoreBase<Machine>
    {
        public MachineStore(MachineProvider provider) : base(provider)
        {
        }
    }
}
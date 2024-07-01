using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineStore : StoreBase<Machine>
    {
        public MachineStore(IProvider<Machine> provider) : base(provider)
        {
        }
    }
}
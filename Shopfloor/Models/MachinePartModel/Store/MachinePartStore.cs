using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartStore : StoreBase<MachinePart>
    {
        public MachinePartStore(IProvider<MachinePart> provider)
            : base(provider)
        {
        }
    }
}
namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartStore : StoreBase<MachinePart>
    {
        public MachinePartStore(MachinePartProvider provider) : base(provider)
        {
        }
    }
}
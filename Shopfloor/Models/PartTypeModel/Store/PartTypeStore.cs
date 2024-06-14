using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypeStore : StoreBase<PartType>
    {
        public PartTypeStore(PartTypeProvider provider) : base(provider)
        {
        }
    }
}
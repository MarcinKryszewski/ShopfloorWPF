using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypeStore : StoreBase<PartType>
    {
        public PartTypeStore(IProvider<PartType> provider)
            : base(provider)
        {
        }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartStore : StoreBase<Part>
    {
        public PartStore(IProvider<Part> provider) : base(provider)
        {
        }
    }
}
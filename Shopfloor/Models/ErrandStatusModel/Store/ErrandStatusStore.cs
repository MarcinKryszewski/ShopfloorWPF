using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusStore : StoreBase<ErrandStatus>
    {
        public ErrandStatusStore(IProvider<ErrandStatus> provider) : base(provider)
        {
        }
    }
}
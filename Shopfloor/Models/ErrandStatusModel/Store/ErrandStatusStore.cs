using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusStore : StoreBase<ErrandStatus>
    {
        public ErrandStatusStore(ErrandStatusProvider provider) : base(provider)
        {
        }
    }
}
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandModel.Store
{
    internal sealed class ErrandStore : StoreBase<Errand>
    {
        public ErrandStore(ErrandProvider provider)
            : base(provider)
        {
        }
    }
}
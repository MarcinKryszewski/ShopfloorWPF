using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandModel.Store
{
    internal sealed class ErrandStore : StoreBase<Errand>
    {
        public ErrandStore(IProvider<Errand> provider)
            : base(provider)
        {
        }
    }
}
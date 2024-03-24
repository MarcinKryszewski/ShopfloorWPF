using Shopfloor.Models.ErrandModel.Store.Combine;

namespace Shopfloor.Models.ErrandModel.Store
{
    internal sealed class ErrandStore : StoreBase<Errand>
    {
        public ErrandStore(ErrandProvider provider, ErrandCombiner combiner)
            : base(provider, combiner)
        {
        }
    }
}
using Shopfloor.Models.ErrandPartModel.Store.Combine;

namespace Shopfloor.Models.ErrandPartModel.Store
{
    internal sealed class ErrandPartStore : StoreBase<ErrandPart>
    {
        public ErrandPartStore(ErrandPartProvider provider, ErrandPartCombiner combiner)
            : base(provider, combiner)
        { }
    }
}
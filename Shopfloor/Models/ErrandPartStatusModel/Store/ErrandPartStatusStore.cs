using Shopfloor.Models.ErrandPartStatusModel.Store.Combine;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusStore : StoreBase<ErrandPartStatus>
    {
        public ErrandPartStatusStore(ErrandPartStatusProvider provider, ErrandPartStatusCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
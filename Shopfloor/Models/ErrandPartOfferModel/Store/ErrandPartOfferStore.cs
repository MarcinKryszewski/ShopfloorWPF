using Shopfloor.Models.ErrandPartModel.Store.Combine;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferStore : StoreBase<ErrandPartOffer>
    {
        public ErrandPartOfferStore(ErrandPartOfferProvider provider, ErrandPartCombiner combiner) : base(provider, combiner)
        {
        }
    }
}
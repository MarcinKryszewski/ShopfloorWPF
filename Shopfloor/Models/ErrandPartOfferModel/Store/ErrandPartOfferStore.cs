using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferStore : StoreBase<ErrandPartOffer>
    {
        public ErrandPartOfferStore(ErrandPartOfferProvider provider) : base(provider)
        {
        }
    }
}
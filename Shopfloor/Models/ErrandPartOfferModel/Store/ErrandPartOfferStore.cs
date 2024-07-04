using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferStore : StoreBase<ErrandPartOffer>
    {
        public ErrandPartOfferStore(IProvider<ErrandPartOffer> provider)
            : base(provider)
        {
        }
    }
}
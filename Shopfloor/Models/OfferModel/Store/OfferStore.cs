using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferStore : StoreBase<Offer>
    {
        public OfferStore(IProvider<Offer> provider)
            : base(provider)
        {
        }
    }
}
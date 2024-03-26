namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferStore : StoreBase<Offer>
    {
        public OfferStore(OfferProvider provider) : base(provider)
        {
        }
    }
}
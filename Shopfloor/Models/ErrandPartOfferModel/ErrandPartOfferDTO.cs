using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OfferModel;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferDTO
    {
        public int? Id { get; set; }
        public ErrandPart? ErrandPart { get; set; }
        public int ErrandPartId { get; set; }
        public Offer? Offer { get; set; }
        public int OfferId { get; set; }
    }
}
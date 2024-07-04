using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OfferModel;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferDto
    {
        public ErrandPart? ErrandPart { get; set; }
        public int ErrandPartId { get; set; }
        public int? Id { get; set; }
        public Offer? Offer { get; set; }
        public int OfferId { get; set; }
    }
}
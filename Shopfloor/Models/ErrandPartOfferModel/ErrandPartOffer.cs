using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OfferModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed partial class ErrandPartOffer : DataModel
    {
        private readonly ErrandPartOfferDto _data;
        public ErrandPartOffer()
        {
            _data = new();
        }
        public ErrandPart? ErrandPart
        {
            get => _data.ErrandPart;
            set
            {
                if (value is null)
                {
                    _data.ErrandPart = null;
                    return;
                }
                if (value.Id == ErrandPartId)
                {
                    _data.ErrandPart = value;
                    return;
                }
                AddError(nameof(ErrandPart), "ErrandPartId do not match");
            }
        }
        required public int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                if (_data.Id is not null)
                {
                    AddError(nameof(Id), "Id already assigned");
                    return;
                }
                _data.Id = value;
            }
        }
        public Offer? Offer
        {
            get => _data.Offer;
            set
            {
                if (value is null)
                {
                    _data.Offer = null;
                    return;
                }
                if (value.Id == OfferId)
                {
                    _data.Offer = value;
                    return;
                }
                AddError(nameof(Offer), "OfferId do not match");
            }
        }
        required public int OfferId
        {
            get => _data.OfferId;
            init => _data.OfferId = value;
        }
    }
}
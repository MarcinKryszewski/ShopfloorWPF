using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.OrderModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed partial class ErrandPartOrder : DataModel
    {
        private readonly ErrandPartOrderDto _data;
        public ErrandPartOrder()
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
        public Order? Order
        {
            get => _data.Order;
            set
            {
                if (value is null)
                {
                    _data.Order = null;
                    return;
                }
                if (value.Id == OrderId)
                {
                    _data.Order = value;
                    return;
                }
                AddError(nameof(Order), "ErrandPartId do not match");
            }
        }
        required public int OrderId
        {
            get => _data.OrderId;
            init => _data.OrderId = value;
        }
    }
}
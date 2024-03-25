using Shopfloor.Models.ErrandPartOrderModel.Store.Combine;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderStore : StoreBase<ErrandPartOrder>
    {
        public ErrandPartOrderStore(ErrandPartOrderProvider provider, ErrandPartOrderCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
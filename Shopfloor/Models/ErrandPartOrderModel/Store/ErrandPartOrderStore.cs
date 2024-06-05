using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderStore : StoreBase<ErrandPartOrder>
    {
        public ErrandPartOrderStore(ErrandPartOrderProvider provider) : base(provider)
        {
        }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderStore : StoreBase<ErrandPartOrder>
    {
        public ErrandPartOrderStore(IProvider<ErrandPartOrder> provider)
            : base(provider)
        {
        }
    }
}
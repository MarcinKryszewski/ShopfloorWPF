using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel.Store
{
    internal sealed class ErrandPartStore : StoreBase<ErrandPart>
    {
        public ErrandPartStore(ErrandPartProvider provider) : base(provider)
        { }
    }
}
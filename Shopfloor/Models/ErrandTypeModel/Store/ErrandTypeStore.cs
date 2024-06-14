using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal class ErrandTypeStore : StoreBase<ErrandType>
    {
        public ErrandTypeStore(ErrandTypeProvider provider) : base(provider)
        {
        }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal class ErrandTypeStore : StoreBase<ErrandType>
    {
        public ErrandTypeStore(IProvider<ErrandType> provider)
            : base(provider)
        {
        }
    }
}
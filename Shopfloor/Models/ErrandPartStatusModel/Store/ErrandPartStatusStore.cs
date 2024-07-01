using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusStore : StoreBase<ErrandPartStatus>
    {
        public ErrandPartStatusStore(IProvider<ErrandPartStatus> provider) : base(provider)
        {
        }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel.Store.Combine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal class ErrandTypeStore : StoreBase<ErrandType>
    {
        public ErrandTypeStore(ErrandTypeProvider provider, ErrandTypeCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel.Store.Combine
{
    internal sealed class ErrandTypeCombiner : ICombiner<ErrandType>
    {
        public Task Combine(List<ErrandType> data)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
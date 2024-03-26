using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel.Store.Combine
{
    internal sealed class ErrandTypeCombiner : ICombinerManager<ErrandType>
    {
        public Task Combine()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
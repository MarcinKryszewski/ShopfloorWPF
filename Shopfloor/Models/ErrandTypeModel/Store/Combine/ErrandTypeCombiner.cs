using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel.Store.Combine
{
    internal sealed class ErrandTypeCombiner : ICombinerManager<ErrandType>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task CombineOne(ErrandType item)
        {
            return Task.CompletedTask;
        }
    }
}
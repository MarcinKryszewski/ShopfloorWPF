using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel.Store.Combine
{
    internal sealed class ErrandStatusCombiner : ICombiner<ErrandStatus>
    {
        public Task Combine(List<ErrandStatus> data)
        {
            return Task.CompletedTask;
        }
    }
}
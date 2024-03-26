using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel.Store.Combine
{
    internal sealed class ErrandStatusCombiner : ICombinerManager<ErrandStatus>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartTypeModel.Store.Combine
{
    internal sealed class PartTypeCombiner : ICombinerManager<PartType>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}
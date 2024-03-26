using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartTypeModel.Store.Combine
{
    internal sealed class PartTypeCombiner : ICombiner<PartType>
    {
        public Task Combine(List<PartType> data)
        {
            return Task.CompletedTask;
        }
    }
}
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartCombiner : ICombiner<Part>
    {
        private readonly PartToPartType _partType;
        public PartCombiner(PartToPartType partType)
        {
            _partType = partType;
        }
        public async Task Combine(List<Part> data)
        {
            List<Task> tasks = [];

            tasks.Add(_partType.Combine(data));

            await Task.WhenAll(tasks);
        }
    }
}
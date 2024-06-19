using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartCombiner : ICombinerManager<Part>
    {
        private readonly PartToPartType _partType;
        public PartCombiner(PartToPartType partType)
        {
            _partType = partType;
        }
        public bool IsCombined { get; private set; }
        public async Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return;
            List<Task> tasks = [];

            tasks.Add(_partType.CombineAll());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
        public async Task CombineOne(Part item)
        {
            List<Task> tasks = [];

            tasks.Add(_partType.CombineOne(item));

            await Task.WhenAll(tasks);
        }
    }
}
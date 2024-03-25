using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartCombiner : ICombiner
    {
        private readonly PartToPartType _partType;
        public PartCombiner(PartToPartType partType)
        {
            _partType = partType;
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_partType.Combine());

            await Task.WhenAll(tasks);
        }
    }
}
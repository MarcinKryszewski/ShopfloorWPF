using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachinePartModel.Store.Combine
{
    internal sealed class MachinePartCombiner : ICombiner<MachinePart>
    {
        public Task Combine(List<MachinePart> data)
        {
            return Task.CompletedTask;
        }
    }
}
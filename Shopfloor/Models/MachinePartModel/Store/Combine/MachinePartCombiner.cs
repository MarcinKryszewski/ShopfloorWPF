using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachinePartModel.Store.Combine
{
    internal sealed class MachinePartCombiner : ICombinerManager<MachinePart>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}
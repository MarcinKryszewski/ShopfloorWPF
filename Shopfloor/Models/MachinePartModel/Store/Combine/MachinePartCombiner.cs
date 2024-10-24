using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MachinePartModel.Store.Combine
{
    internal sealed class MachinePartCombiner : ICombinerManager<MachinePart>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return Task.CompletedTask;
            }

            IsCombined = true;
            return Task.CompletedTask;
        }
        public Task CombineOne(MachinePart item)
        {
            return Task.CompletedTask;
        }
    }
}
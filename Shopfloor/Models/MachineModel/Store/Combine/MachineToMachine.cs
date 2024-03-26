using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel.Store.Combine
{
    internal sealed class MachineToMachine : ICombiner<Machine>
    {
        public MachineToMachine()
        {
        }
        public Task Combine(List<Machine> data)
        {
            foreach (Machine machine in data)
            {
                if (machine.ParentId is not null)
                {
                    Machine? parent = data.FirstOrDefault(m => m.Id == machine.ParentId);
                    if (parent is not null) machine.SetParent(parent);
                }
            }
            return Task.CompletedTask;
        }
    }
}
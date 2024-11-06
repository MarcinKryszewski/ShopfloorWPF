using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Instructions
{
    internal class InstructionStore : IStore<Instruction>
    {
        private readonly List<Instruction> _data = [];
        public List<Instruction> Data => _data;
        public Task AddItem(Instruction item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
            await Task.Delay(0);
        }
    }
}
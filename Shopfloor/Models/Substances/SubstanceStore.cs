using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Substances
{
    internal class SubstanceStore : IStore<Substance>
    {
        private readonly List<Substance> _data = [];
        public List<Substance> Data => _data;
        public Task AddItem(Substance item)
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
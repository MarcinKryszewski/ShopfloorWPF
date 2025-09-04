using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HPhrases
{
    internal class HPhraseStore : IStore<HPhrase>
    {
        private readonly List<HPhrase> _data = [];
        public List<HPhrase> Data => _data;
        public Task AddItem(HPhrase item)
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
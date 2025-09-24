using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PPhrases
{
    internal class PPhraseStore : IStore<PPhrase>
    {
        private readonly List<PPhrase> _data = [];
        public List<PPhrase> Data => _data;
        public Task AddItem(PPhrase item)
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
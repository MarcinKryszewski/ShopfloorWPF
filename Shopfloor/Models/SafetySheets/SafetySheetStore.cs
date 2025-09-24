using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SafetySheets
{
    internal class SafetySheetStore : IStore<SafetySheet>
    {
        private readonly List<SafetySheet> _data = [];
        public List<SafetySheet> Data => _data;
        public Task AddItem(SafetySheet item)
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.H1Sheets
{
    internal class H1SheetStore : IStore<H1Sheet>
    {
        private readonly List<H1Sheet> _data = [];
        public List<H1Sheet> Data => _data;
        public Task AddItem(H1Sheet item)
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
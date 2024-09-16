using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineStore : IStore<LineModel>
    {
        private List<LineModel> _data = [];
        public List<LineModel> Data => _data;
        public Task AddItem(LineModel item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
        }
    }
}
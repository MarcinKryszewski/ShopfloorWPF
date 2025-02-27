using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Attendences
{
    internal class AttendenceStore : IStore<Attendence>
    {
        private readonly List<Attendence> _data = [];
        public List<Attendence> Data => _data;
        public Task AddItem(Attendence item)
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
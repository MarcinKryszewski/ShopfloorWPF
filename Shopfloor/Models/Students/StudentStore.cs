using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Students
{
    internal class StudentStore : IStore<Student>
    {
        private readonly List<Student> _data = [];
        public List<Student> Data => _data;
        public Task AddItem(Student item)
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
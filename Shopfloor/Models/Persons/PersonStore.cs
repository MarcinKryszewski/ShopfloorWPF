using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Persons
{
    internal class PersonStore : IStore<Person>
    {
        private readonly List<Person> _data = [];
        public List<Person> Data => _data;
        public Task AddItem(Person item)
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
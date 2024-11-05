using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IStore<T>
        where T : IModel
    {
        public List<T> Data { get; }
        public Task AddItem(T item);
        //public Task<List<T>> GetDataAsync();
        public Task ReloadData();
    }
}
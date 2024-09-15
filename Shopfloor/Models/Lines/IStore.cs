using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.Commons.Interfaces
{
    public interface IStore<T>
        where T : IModel
    {
        public HashSet<Type> Merges { get; }
        public Task AddItem(T item);
        public Task<List<T>> GetDataAsync();
        public Task ReloadData();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface IProvider<T>
    {
        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> GetAll();

        public Task<int> Create(T item);

        public Task Update(T item);

        public Task Delete(int id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.Commons.Interfaces
{
    public interface IProvider<T>
        where T : IModel
    {
        public Task<int> Create(T item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Update(T item);
        public Task Delete(int id);
    }
}
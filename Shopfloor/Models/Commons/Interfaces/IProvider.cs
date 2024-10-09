using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IProvider<T, TCreate>
        where T : IModel
        where TCreate : IModelCreationModel<T>
    {
        public Task<int> Create(TCreate item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Update(T item);
        public Task Delete(int id);
    }
}
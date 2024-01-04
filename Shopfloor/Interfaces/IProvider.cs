using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    public interface IProvider<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}
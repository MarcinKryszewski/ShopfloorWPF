using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.Interfaces
{
    public interface IStore<T>
        where T : IModel
    {
        public Task<List<T>> GetDataAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.WorkOrders
{
    public interface IRepository<T>
        where T : IModel
    {
        public Task<List<T>> GetData();
        public Task<T> Create();
        public Task<T> Update();
        public Task<T> Delete();
    }
}
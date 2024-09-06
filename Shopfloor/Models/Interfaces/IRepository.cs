using System.Threading.Tasks;

namespace Shopfloor.Models.WorkOrders
{
    public interface IRepository<T>
        where T : IModel
    {
        public Task LoadData();
        public Task RefreshData();
        public Task<T> Create();
        public Task<T> Update();
        public Task<T> Delete();
    }
}
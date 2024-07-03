using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces.Models
{
    internal interface IDataModelStoreService<T> where T : DataModel
    {
        public void Add(T item);
        public void Edit(T item);
        public void Remove(T item);
        public T? FindItem(T item);
    }
}
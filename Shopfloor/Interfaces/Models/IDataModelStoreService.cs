using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces
{
    internal interface IDataModelStoreService<T> where T : DataModel
    {
        public void AddToStore(T item);
    }
}
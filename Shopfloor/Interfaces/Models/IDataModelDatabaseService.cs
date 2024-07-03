using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces
{
    internal interface IDataModelDatabaseService<T> where T : DataModel
    {
        public int Add(T item);
        public void Edit(T item);
        public void Delete(T item);
    }
}
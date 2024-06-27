using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces
{
    internal interface IDataModelDatabaseService<T> where T : DataModel
    {
        public int AddToDatabase(T item);
        public void EditInDatabase(T item);
    }
}
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces
{
    internal interface IModelCreatorService<in T>
        where T : DataModel
    {
        public void Create(T item);
    }
}
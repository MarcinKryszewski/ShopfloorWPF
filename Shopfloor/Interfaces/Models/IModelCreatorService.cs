using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces
{
    internal interface IModelCreatorService<T> where T : DataModel
    {
        public void Create(T item);
    }
}
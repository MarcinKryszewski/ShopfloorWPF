using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    public interface IModelDeleterService<T> where T : DataModel
    {
        public void Delete(T item);
    }
}
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    public interface IModelDeleterService<in T>
        where T : DataModel
    {
        public void Delete(T item);
    }
}
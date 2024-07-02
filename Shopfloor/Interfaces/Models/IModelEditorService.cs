using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces.Models
{
    internal interface IModelEditorService<T> where T : DataModel
    {
        public void Edit(T item);
    }
}
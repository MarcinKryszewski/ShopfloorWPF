using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Interfaces.Models
{
    internal interface IModelEditorService<in T>
        where T : DataModel
    {
        public void Edit(T item);
    }
}
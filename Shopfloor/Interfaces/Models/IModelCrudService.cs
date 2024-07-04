using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal interface IModelCrudService<in T>
        where T : DataModel
    {
        public IModelCreatorService<T> Creator { get; }
        public IModelDeleterService<T> Deleter { get; }
        public IModelEditorService<T> Editor { get; }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal interface IModelCrudService<T> where T : DataModel
    {
        public IModelCreatorService<T> Creator { get; }
        public IModelEditorService<T> Editor { get; }
        public IModelDeleterService<T> Destroyer { get; }
    }
}
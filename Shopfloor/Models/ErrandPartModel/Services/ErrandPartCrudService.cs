using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartCrudService : IModelCrudService<ErrandPart>
    {
        public ErrandPartCrudService(
            IModelEditorService<ErrandPart> editor,
            IModelCreatorService<ErrandPart> creator,
            IModelDeleterService<ErrandPart> deleter)
        {
            Editor = editor;
            Creator = creator;
            Deleter = deleter;
        }
        public IModelCreatorService<ErrandPart> Creator { get; }
        public IModelDeleterService<ErrandPart> Deleter { get; }
        public IModelEditorService<ErrandPart> Editor { get; }
    }
}
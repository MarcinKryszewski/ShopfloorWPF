using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartCrudService : IModelCrudService<ErrandPart>
    {
        public IModelCreatorService<ErrandPart> Creator { get; }
        public IModelEditorService<ErrandPart> Editor { get; }
        public IModelDeleterService<ErrandPart> Destroyer { get; }

        public ErrandPartCrudService(
            IModelEditorService<ErrandPart> editor,
            IModelCreatorService<ErrandPart> creator,
            IModelDeleterService<ErrandPart> destroyer)
        {
            Editor = editor;
            Creator = creator;
            Destroyer = destroyer;
        }
    }
}
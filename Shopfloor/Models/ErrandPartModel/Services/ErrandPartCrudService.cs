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
        public IModelDeleterService<ErrandPart> Deleter { get; }

        public ErrandPartCrudService(
            IModelEditorService<ErrandPart> editor,
            IModelCreatorService<ErrandPart> creator,
            IModelDeleterService<ErrandPart> deleter)
        {
            Editor = editor;
            Creator = creator;
            Deleter = deleter;
        }
    }
}
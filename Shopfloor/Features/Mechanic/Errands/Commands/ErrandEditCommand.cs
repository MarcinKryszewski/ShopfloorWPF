using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly IModelEditorService<Errand> _errandEditor;
        public event Action<bool>? ErrandEdited;
        private readonly Errand _originalErrand;
        public ErrandEditCommand(IModelEditorService<Errand> errandEditor, Errand originalErrand)
        {
            _errandEditor = errandEditor;
            _originalErrand = originalErrand;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            int userId = creatorData.UserId;

            EditErrand(creatorData.Errand);
            ManageErrandParts(creatorData.Errand);

            ErrandEdited?.Invoke(true);
        }
        private void EditErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            _errandEditor.Edit(errand);
        }
        private void ManageErrandParts(Errand errand)
        {
            List<ErrandPart> newParts = errand.Parts;
            List<ErrandPart> existingParts = _originalErrand.Parts;

            List<ErrandPart> partsToAdd = newParts.Except(existingParts).ToList();
            List<ErrandPart> partsToRemove = newParts.Except(existingParts).ToList();
            List<ErrandPart> partsToEdit = existingParts.Intersect(newParts).ToList();
        }
    }
}
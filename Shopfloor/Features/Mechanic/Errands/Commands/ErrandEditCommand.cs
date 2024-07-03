using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly IModelEditorService<Errand> _errandEditor;
        private readonly IModelCreatorService<ErrandStatus> _statusCreator;
        private readonly IModelCreatorService<ErrandPart> _partCreator;
        private readonly IModelEditorService<ErrandPart> _partEditor;
        public event Action<bool>? ErrandEdited;
        private readonly Errand _originalErrand;
        public ErrandEditCommand(
            IModelEditorService<Errand> errandEditor,
            Errand originalErrand,
            IModelCrudService<ErrandPart> errandPartCrud,
            IModelCreatorService<ErrandStatus> statusCreator)
        {
            _errandEditor = errandEditor;
            _originalErrand = originalErrand;
            _partCreator = errandPartCrud.Creator;
            _statusCreator = statusCreator;
            _partEditor = errandPartCrud.Editor;
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
            List<ErrandPart> partsToRemove = existingParts.Except(newParts).ToList();
            List<ErrandPart> partsToEdit = existingParts.Intersect(newParts).ToList();

            AddParts(partsToAdd, errand);
            RemoveParts(partsToRemove);
            EditParts(partsToEdit);
        }
        private void EditParts(List<ErrandPart> parts)
        {

        }
        private void RemoveParts(List<ErrandPart> parts)
        {
        }
        private void AddParts(List<ErrandPart> parts, Errand errand)
        {
            if (parts.Count == 0) return;
            foreach (ErrandPart part in parts)
            {
                part.ErrandId = (int)errand.Id!;
                _partCreator.Create(part);
            }
            SetPartsSpecifiedStatus(errand);
        }
        private void SetPartsSpecifiedStatus(Errand errand)
        {
            string partsSpecified = "SYSTEM: PARTS SPECIFIED";
            ErrandStatus errandStatus = new()
            {
                ErrandId = (int)errand.Id!,
                SetDate = DateTime.Now,
                StatusName = ErrandStatusList.PartsListCompleted,
                Reason = partsSpecified
            };
            errand.AddStatus(errandStatus);
            _statusCreator.Create(errandStatus);
        }
    }
}
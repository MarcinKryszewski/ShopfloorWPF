using System;
using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly IModelEditorService<Errand> _errandEditor;
        private readonly Errand _originalErrand;
        private readonly IModelCreatorService<ErrandPart> _partCreator;
        private readonly IModelDeleterService<ErrandPart> _partDeleter;
        private readonly IModelEditorService<ErrandPart> _partEditor;
        private readonly IModelCreatorService<ErrandStatus> _statusCreator;
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
            _partDeleter = errandPartCrud.Deleter;
        }
        public event Action<bool>? ErrandEdited;
        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            int userId = creatorData.UserId;

            EditErrand(creatorData.Errand);
            ManageErrandParts(creatorData.Errand);

            ErrandEdited?.Invoke(true);
        }
        private void AddParts(List<ErrandPart> parts, Errand errand)
        {
            if (parts.Count == 0)
            {
                return;
            }

            foreach (ErrandPart part in parts)
            {
                part.ErrandId = (int)errand.Id!;
                _partCreator.Create(part);
            }
            SetPartsStatus(errand, ErrandStatusList.PartsListCompleted);
        }
        private void EditErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors)
            {
                return;
            }

            _errandEditor.Edit(errand);
        }
        private void EditParts(List<ErrandPart> parts, Errand errand)
        {
            if (parts.Count == 0)
            {
                return;
            }

            foreach (ErrandPart item in parts)
            {
                _partEditor.Edit(item);
            }
            SetPartsStatus(errand, ErrandStatusList.ErrandEdited);
        }
        private void ManageErrandParts(Errand errand)
        {
            List<ErrandPart> newParts = errand.Parts;
            List<ErrandPart> existingParts = _originalErrand.Parts;

            List<ErrandPart> partsToAdd = newParts.Except(existingParts).ToList();
            List<ErrandPart> partsToRemove = existingParts.Except(newParts).ToList();
            // List<ErrandPart> partsToEdit = existingParts.Intersect(newParts).ToList();

            List<ErrandPart> partsToEdit = existingParts
            .Join(
                newParts,
                itemA => itemA.Id,
                itemB => itemB.Id,
                (itemA, itemB) => new { itemA, itemB })
            .Where(joined => joined.itemB.Amount != joined.itemA.Amount)
            .Select(joined => joined.itemB)
            .ToList();

            EditParts(partsToEdit, errand);
            AddParts(partsToAdd, errand);
            RemoveParts(partsToRemove, errand);
        }
        private void RemoveParts(List<ErrandPart> parts, Errand errand)
        {
            if (parts.Count == 0)
            {
                return;
            }

            foreach (ErrandPart item in parts)
            {
                _partDeleter.Delete(item);
            }

            if (errand.Parts.Count == 0)
            {
                SetPartsStatus(errand, ErrandStatusList.NoPartsList);
            }
        }
        private void SetPartsStatus(Errand errand, string status)
        {
            string partsSpecified = "SYSTEM";
            ErrandStatus errandStatus = new()
            {
                ErrandId = (int)errand.Id!,
                SetDate = DateTime.Now,
                StatusName = status,
                Reason = partsSpecified,
            };
            errand.AddStatus(errandStatus);
            _statusCreator.Create(errandStatus);
        }
    }
}
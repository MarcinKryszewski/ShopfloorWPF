using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
using Shopfloor.Models.ErrandStatusModel;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly IModelCreatorService<Errand> _errandCreator;
        private readonly IModelCreatorService<ErrandPart> _partCreator;
        private readonly IModelEditorService<ErrandStatus> _statusEditorService;
        private readonly IModelCreatorService<ErrandStatus> _statusCreator;

        public event Action? ErrandCreated;
        public ErrandNewCommand(IModelCreatorService<Errand> errandCreator, IModelCreatorService<ErrandPart> partCreator, IModelEditorService<ErrandStatus> errandStatusEditorService, IModelCreatorService<ErrandStatus> statusCreator)
        {
            _errandCreator = errandCreator;
            _partCreator = partCreator;
            _statusEditorService = errandStatusEditorService;
            _statusCreator = statusCreator;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            int userId = creatorData.UserId;
            AddErrand(creatorData.Errand);
            AddErrandParts(creatorData.Parts, creatorData.Errand);
            ErrandCreated?.Invoke();
        }
        private void AddErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            _errandCreator.Create(errand);
        }
        private void AddErrandParts(List<ErrandPart> parts, Errand errand)
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
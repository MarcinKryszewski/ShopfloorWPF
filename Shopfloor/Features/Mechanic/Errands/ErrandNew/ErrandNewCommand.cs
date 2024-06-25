using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly IModelCreatorService<Errand> _errandCreator;
        private readonly IModelCreatorService<ErrandPart> _partCreator;
        public event Action? ErrandCreated;
        public ErrandNewCommand(IModelCreatorService<Errand> errandCreator, IModelCreatorService<ErrandPart> partCreator)
        {
            _errandCreator = errandCreator;
            _partCreator = partCreator;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            AddErrand(creatorData.Errand);
            AddErrandParts(creatorData.Parts, (int)creatorData.Errand.Id!);
        }
        private void AddErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            _errandCreator.Create(errand);
            ErrandCreated?.Invoke();
        }
        private void AddErrandParts(List<Part> parts, int id)
        {
            foreach (Part part in parts)
            {
                _partCreator.Create(new()
                {
                    ErrandId = id,
                    PartId = (int)part.Id!,
                });
            }
        }
    }
}
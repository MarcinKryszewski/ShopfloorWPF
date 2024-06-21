using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System.Collections.Generic;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandModel.Services;
using System;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly IErrandCreatorService _errandCreator;
        public event Action? ErrandCreated;
        public ErrandNewCommand(IErrandCreatorService errandCreator)
        {
            _errandCreator = errandCreator;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            AddErrand(creatorData.Errand);
        }
        private void AddErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            _errandCreator.Create(errand);
            ErrandCreated?.Invoke();
        }
    }
}
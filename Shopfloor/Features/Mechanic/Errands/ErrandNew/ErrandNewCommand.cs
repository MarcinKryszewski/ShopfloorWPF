using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System.Collections.Generic;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly ErrandStore _errandStore;
        private readonly IProvider<Errand> _errandProvider;
        public ErrandNewCommand(ErrandStore errandStore, IProvider<Errand> errandProvider, List<Part> parts, ErrandPartProvider errandPartProvider, ErrandStatusProvider errandStatusProvider, ErrandPartStore errandPartStore, ErrandStatusStore errandStatusStore)
        {
            _errandStore = errandStore;
            _errandProvider = errandProvider;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            Errand errand = (Errand)parameter;
            AddErrand(errand);
        }
        private void AddErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            int errandId = _errandProvider.Create(errand).Result;

            errand.SetId(errandId);
            _errandStore.Data.Add(errand);
        }
    }
}
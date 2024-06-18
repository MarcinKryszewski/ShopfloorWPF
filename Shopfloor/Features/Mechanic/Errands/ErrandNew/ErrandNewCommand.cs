using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly ErrandStore _errandStore;
        private readonly ErrandProvider _errandProvider;
        public ErrandNewCommand(ErrandStore errandStore, ErrandProvider errandProvider)
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
            if (errand.HasErrors) return;

            int errandId = _errandProvider.Create(errand).Result;

            errand.SetId(errandId);
            _errandStore.Data.Add(errand);
        }
    }
}
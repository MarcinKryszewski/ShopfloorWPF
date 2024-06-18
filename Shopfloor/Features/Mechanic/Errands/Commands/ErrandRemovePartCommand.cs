using Shopfloor.Features.Mechanic.Errands.ErrandPartsList;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Shared.Commands;
using System.Linq;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandRemovePartCommand : CommandBase
    {
        private readonly ErrandPartsListViewModel _viewModel;
        private readonly SelectedErrandStore _errandStore;

        public ErrandRemovePartCommand(ErrandPartsListViewModel viewModel, SelectedErrandStore errandStore)
        {
            _viewModel = viewModel;
            _errandStore = errandStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            if (parameter.GetType() != typeof(int)) return;
            ErrandPart errandPart = _errandStore.ErrandParts.First((ep) => ep.PartId == (int)parameter);
            _errandStore.ErrandParts.Remove(errandPart);
            _viewModel.ErrandParts.Refresh();
        }
    }
}
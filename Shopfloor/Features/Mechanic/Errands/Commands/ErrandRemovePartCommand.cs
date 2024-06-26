using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;
using System.Collections.Generic;
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
            if (_viewModel.ErrandData is null) return;

            int partId = (int)parameter;
            List<ErrandPart> parts = _viewModel.ErrandData.Parts;
            ErrandPart? errandPart = parts.FirstOrDefault((p) => p.PartId == partId);
            if (errandPart != null) parts.Remove(errandPart);

            _viewModel.ErrandParts.Refresh();
        }
    }
}
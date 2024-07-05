using System.Collections.Generic;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandRemovePartCommand : CommandBase
    {
        private readonly SelectedErrandStore _errandStore;
        private readonly ErrandPartsListViewModel _viewModel;
        public ErrandRemovePartCommand(
            ErrandPartsListViewModel viewModel,
            SelectedErrandStore errandStore)
        {
            _viewModel = viewModel;
            _errandStore = errandStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            if (parameter is not int)
            {
                return;
            }

            if (_viewModel.ErrandData is null)
            {
                return;
            }

            int partId = (int)parameter;
            List<ErrandPart> parts = _viewModel.ErrandData.Errand.Parts;

            int position = parts.FindIndex(p => p.PartId == partId);
            if (position != -1)
            {
                parts.RemoveAt(position);
            }

            _viewModel.ErrandParts.Refresh();
        }
    }
}
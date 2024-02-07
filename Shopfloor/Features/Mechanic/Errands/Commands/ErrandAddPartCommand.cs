using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shopfloor.Features.Mechanic.Errands.ErrandPartsList;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandAddPartCommand : CommandBase
    {
        private readonly ErrandPartsListViewModel _viewModel;
        private readonly SelectedErrandStore _errandStore;

        public ErrandAddPartCommand(ErrandPartsListViewModel viewModel, SelectedErrandStore errandStore)
        {
            _viewModel = viewModel;
            _errandStore = errandStore;
        }

        public override void Execute(object? parameter)
        {
            Part? selectedPart = _viewModel.SelectedPart;
            ObservableCollection<ErrandPart> errandParts = _errandStore.ErrandParts;

            if (selectedPart is null) return;
            if (selectedPart.Id is null) return;

            if (errandParts.FirstOrDefault((p) => p.PartId == selectedPart.Id) == null)
            {
                ErrandPart errandPart = new(0, (int)selectedPart.Id)
                {
                    Part = selectedPart
                };
                errandParts.Add(errandPart);
                _viewModel.ErrandParts.Refresh();
            }
        }
    }
}
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandAddPartCommand : CommandBase
    {
        private readonly ErrandPartsListViewModel _viewModel;
        //private ErrandCreatorData _errandCreatorData;

        public ErrandAddPartCommand(ErrandPartsListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;

            Part? selectedPart = _viewModel.SelectedPart;

            if (selectedPart is null) return;
            if (selectedPart.Id is null) return;

            if (creatorData.Parts.FirstOrDefault((ep) => ep.PartId == selectedPart.Id) == null)
            {
                ErrandPart errandPart = new()
                {
                    PartId = (int)selectedPart.Id,
                    Part = selectedPart,
                    ErrandId = 0,
                    OrderedById = creatorData.UserId
                };
                creatorData.Parts.Add(errandPart);
                _viewModel.ErrandParts.Refresh();
            }
        }
    }
}
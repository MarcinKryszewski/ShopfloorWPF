using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

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
            if (parameter is null)
            {
                return;
            }

            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;

            Part? selectedPart = _viewModel.SelectedPart;

            if (selectedPart is null)
            {
                return;
            }

            if (selectedPart.Id is null)
            {
                return;
            }

            if (creatorData.Parts.Find((ep) => ep.PartId == selectedPart.Id) == null)
            {
                ErrandPart errandPart = new()
                {
                    PartId = (int)selectedPart.Id,
                    Part = selectedPart,
                    ErrandId = (int)creatorData.Errand.Id!,
                    OrderedById = creatorData.UserId,
                };
                creatorData.Errand.Parts.Add(errandPart);
                _viewModel.ErrandParts.Refresh();
            }
        }
    }
}
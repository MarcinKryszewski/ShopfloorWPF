using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandsShowPartsList : CommandBase
    {
        private readonly IPartsList _viewModel;
        private readonly ErrandPartsListViewModel _partsViewModel;

        public ErrandsShowPartsList(IPartsList viewModel, ErrandPartsListViewModel partsViewModel)
        {
            _viewModel = viewModel;
            _partsViewModel = partsViewModel;
        }

        public override void Execute(object? parameter)
        {
            // if (parameter is null) return;
            // ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            // _partsViewModel.ErrandData = creatorData;

            _viewModel.PartsList = _viewModel.PartsList is null ? _partsViewModel : null;
        }
    }
}
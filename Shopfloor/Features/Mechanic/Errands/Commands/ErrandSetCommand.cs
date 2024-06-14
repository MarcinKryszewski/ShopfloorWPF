using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandSetCommand : CommandBase
    {
        private readonly ErrandsListViewModel _viewModel;
        private readonly SelectedErrandStore _errandStore;

        public ErrandSetCommand(ErrandsListViewModel viewModel, SelectedErrandStore selectedErrandStore)
        {
            _viewModel = viewModel;
            _errandStore = selectedErrandStore;
        }

        public override void Execute(object? parameter)
        {
            _errandStore.SelectedErrand = _viewModel.SelectedErrand;

            //NavigationService<ErrandsEditViewModel> navigationService = _mainServices.GetRequiredService<NavigationService<ErrandsEditViewModel>>();
            //navigationService.Navigate();
        }
    }
}
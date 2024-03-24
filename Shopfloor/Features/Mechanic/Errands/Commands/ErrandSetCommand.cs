using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Shared.Commands;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandSetCommand : CommandBase
    {
        private readonly ErrandsListViewModel _viewModel;
        private readonly SelectedErrandStore _errandStore;
        private readonly IServiceProvider _mainServices;

        public ErrandSetCommand(ErrandsListViewModel viewModel, IServiceProvider mainServices)
        {
            _viewModel = viewModel;
            _mainServices = mainServices;
            _errandStore = _mainServices.GetRequiredService<SelectedErrandStore>();
        }

        public override void Execute(object? parameter)
        {
            _errandStore.SelectedErrand = _viewModel.SelectedErrand;

            //NavigationService<ErrandsEditViewModel> navigationService = _mainServices.GetRequiredService<NavigationService<ErrandsEditViewModel>>();
            //navigationService.Navigate();
        }
    }
}
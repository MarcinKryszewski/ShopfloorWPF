using Shopfloor.Features.Mechanic.Errands.ErrandsEdit;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly ErrandsEditViewModel _viewModel;
        private readonly IServiceProvider _databaseServices;
        private readonly CurrentUserStore _currentUserStore;
        private readonly SelectedErrandStore _selectedErrand;

        public ErrandEditCommand(ErrandsEditViewModel viewModel, IServiceProvider databaseServices, CurrentUserStore currentUserStore, SelectedErrandStore selectedErrand)
        {
            _viewModel = viewModel;
            _databaseServices = databaseServices;
            _currentUserStore = currentUserStore;
            _selectedErrand = selectedErrand;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
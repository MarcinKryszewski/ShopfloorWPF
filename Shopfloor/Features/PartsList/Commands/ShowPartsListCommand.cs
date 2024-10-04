using System.Windows;
using Shopfloor.Features.PartsList.Interfaces;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkOrderAddNew.Commands
{
    internal class ShowPartsListCommand : CommandBase
    {
        private readonly IViewModelContainingPartsList _partsListVisible;

        public ShowPartsListCommand(IViewModelContainingPartsList viewModel)
        {
            _partsListVisible = viewModel;
        }
        public override void Execute(object? parameter)
        {
            // _partsListVisible.IsPartsListVisible = Visibility.Collapsed;
            _partsListVisible.IsPartsListVisible = Visibility.Visible;
        }
    }
}
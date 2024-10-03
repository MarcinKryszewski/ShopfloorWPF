using System.Windows;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkOrderAddNew.Commands
{
    internal class ShowPartsListCommand : CommandBase
    {
        private readonly WorkOrderAddNewViewModel _partsListVisible;

        public ShowPartsListCommand(WorkOrderAddNewViewModel viewModel)
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
using Shopfloor.Features.Admin.PartTypes.List;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.PartTypes.Commands
{
    public class CleanFormCommand : CommandBase
    {
        private readonly PartTypesListViewModel _viewModel;

        public CleanFormCommand(PartTypesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CleanForm();
        }
    }
}
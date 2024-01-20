using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartCleanFormCommand : CommandBase
    {
        private IInputForm<Part> _viewModel;

        public PartCleanFormCommand(IInputForm<Part> viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CleanForm();
        }
    }
}
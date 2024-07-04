using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    internal sealed class PartCleanFormCommand : CommandBase
    {
        private readonly IInputForm<Part> _viewModel;

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
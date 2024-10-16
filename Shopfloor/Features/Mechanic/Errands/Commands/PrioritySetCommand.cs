using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class PrioritySetCommand : CommandBase
    {
        private readonly IErrandPriority _viewModel;

        public PrioritySetCommand(IErrandPriority viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            if (!(parameter is string))
            {
                return;
            }

            _viewModel.SelectedPriority = (string)parameter;
        }
    }
}
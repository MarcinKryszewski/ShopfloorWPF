using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartTypeCreateCommand : AsyncCommandBase
    {
        private readonly PartTypeProvider _provider;
        private readonly PartsViewModel _viewModel;

        public PartTypeCreateCommand(PartTypeProvider provider, PartsViewModel viewModel)
        {
            _provider = provider;
            _viewModel = viewModel;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            await _viewModel.PartType.Add(_provider);
        }
    }
}
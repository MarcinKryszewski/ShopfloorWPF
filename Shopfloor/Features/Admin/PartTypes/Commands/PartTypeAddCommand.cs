using Shopfloor.Features.Admin.PartTypes.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.PartTypes.Commands
{
    public class PartTypeAddCommand : CommandBase
    {
        private readonly PartTypesListViewModel _viewModel;
        private readonly PartTypeProvider _provider;

        public PartTypeAddCommand(PartTypesListViewModel viewModel, PartTypeProvider provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            PartType partType = new(_viewModel.Name);
            if (!_viewModel.IsDataValidate(partType)) return;

            _viewModel.CleanForm();

            Task.Run(async () =>
            {
                await _provider.Create(partType);
                _ = _viewModel.UpdateData();
            });
        }
    }
}
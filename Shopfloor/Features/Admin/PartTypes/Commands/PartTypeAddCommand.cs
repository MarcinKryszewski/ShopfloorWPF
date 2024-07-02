using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared.Commands;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.PartTypes.Commands
{
    internal sealed class PartTypeAddCommand : CommandBase
    {
        private readonly PartTypesListViewModel _viewModel;
        private readonly IProvider<PartType> _provider;

        public PartTypeAddCommand(PartTypesListViewModel viewModel, IProvider<PartType> provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            PartType partType = new(_viewModel.Name);
            if (!_viewModel.IsDataValidate) return;

            _viewModel.CleanForm();

            Task.Run(async () =>
            {
                await _provider.Create(partType);
                _ = _viewModel.UpdateData();
            });
        }
    }
}
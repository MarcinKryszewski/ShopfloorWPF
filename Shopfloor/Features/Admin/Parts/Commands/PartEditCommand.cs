using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    internal sealed class PartEditCommand : CommandBase
    {
        private readonly PartsEditViewModel _viewModel;
        private readonly PartProvider _partProvider;

        public PartEditCommand(PartsEditViewModel partsEditViewModel, PartProvider partProvider)
        {
            _viewModel = partsEditViewModel;
            _partProvider = partProvider;
        }

        public override void Execute(object? parameter)
        {
            Part part = new(
                _viewModel.Id,
                _viewModel.NamePl,
                _viewModel.NameOriginal,
                _viewModel.TypeId,
                _viewModel.Index,
                _viewModel.Number,
                _viewModel.Details,
                _viewModel.ProducerId,
                _viewModel.SupplierId
            );
            if (!_viewModel.IsDataValidate) return;

            _partProvider.Update(part).Wait();
            _viewModel.ReloadData();
            //_viewModel.CleanForm();
        }
    }
}
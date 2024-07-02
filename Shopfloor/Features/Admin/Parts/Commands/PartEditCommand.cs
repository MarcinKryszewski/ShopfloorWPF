using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    internal sealed class PartEditCommand : CommandBase
    {
        private readonly PartsEditViewModel _viewModel;
        private readonly IProvider<Part> _partProvider;

        public PartEditCommand(PartsEditViewModel partsEditViewModel, IProvider<Part> partProvider)
        {
            _viewModel = partsEditViewModel;
            _partProvider = partProvider;
        }

        public override void Execute(object? parameter)
        {
            Part part = new()
            {
                Id = _viewModel.Id,
                NamePl = _viewModel.NamePl,
                NameOriginal = _viewModel.NameOriginal,
                TypeId = _viewModel.TypeId ?? 0,
                Index = _viewModel.Index,
                ProducerNumber = _viewModel.Number,
                Details = _viewModel.Details,
                ProducerId = _viewModel.ProducerId ?? 0,
                SupplierId = _viewModel.SupplierId
            };

            if (!_viewModel.IsDataValidate) return;

            _partProvider.Update(part).Wait();
            _viewModel.ReloadData();
            //_viewModel.CleanForm();
        }
    }
}
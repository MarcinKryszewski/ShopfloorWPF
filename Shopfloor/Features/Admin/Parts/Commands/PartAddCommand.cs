using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    internal sealed class PartAddCommand : CommandBase
    {
        private readonly PartsAddViewModel _viewModel;
        private readonly IProvider<Part> _partProvider;

        public PartAddCommand(PartsAddViewModel partsAddViewModel, IProvider<Part> provider)
        {
            _viewModel = partsAddViewModel;
            _partProvider = provider;
        }

        public override void Execute(object? parameter)
        {
            Part part = new ()
            {
                NamePl = _viewModel.NamePl,
                NameOriginal = _viewModel.NameOriginal,
                TypeId = _viewModel.TypeId,
                Index = _viewModel.Index,
                ProducerNumber = _viewModel.Number,
                Details = _viewModel.Details,
                ProducerId = _viewModel.ProducerId,
                SupplierId = _viewModel.SupplierId,
                Unit = _viewModel.Unit,
            };

            if (!_viewModel.IsDataValidate)
            {
                return;
            }

            _ = _partProvider.Create(part);

            _viewModel.ReloadData();
            _viewModel.CleanForm();
        }
    }
}
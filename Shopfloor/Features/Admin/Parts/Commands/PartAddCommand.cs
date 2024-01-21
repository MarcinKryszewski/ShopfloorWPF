using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Add;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartAddCommand : CommandBase
    {
        private readonly PartsAddViewModel _viewModel;
        private readonly PartProvider _partProvider;

        public PartAddCommand(PartsAddViewModel partsAddViewModel, IServiceProvider databaseServices)
        {
            _viewModel = partsAddViewModel;
            _partProvider = databaseServices.GetRequiredService<PartProvider>();
        }

        public override void Execute(object? parameter)
        {
            Part part = new(
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

            _ = _partProvider.Create(part);

            _viewModel.ReloadData();
            _viewModel.CleanForm();
        }
    }
}
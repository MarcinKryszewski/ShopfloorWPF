using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Edit;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartEditCommand : CommandBase
    {
        private PartsEditViewModel _viewModel;
        private IServiceProvider _databaseServices;

        public PartEditCommand(PartsEditViewModel partsEditViewModel, IServiceProvider databaseServices)
        {
            _viewModel = partsEditViewModel;
            _databaseServices = databaseServices;
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
            if (!_viewModel.IsDataValidate(part)) return;

            _ = _databaseServices.GetRequiredService<PartProvider>().Update(part);
            _viewModel.ReloadData();
            //_viewModel.CleanForm();
        }
    }
}
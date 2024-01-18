using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Add;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartAddCommand : CommandBase
    {
        private PartsAddViewModel _viewModel;
        private IServiceProvider _databaseServices;

        public PartAddCommand(PartsAddViewModel partsAddViewModel, IServiceProvider databaseServices)
        {
            _viewModel = partsAddViewModel;
            _databaseServices = databaseServices;
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
            if (!_viewModel.IsDataValidate(part)) return;

            _ = _databaseServices.GetRequiredService<PartProvider>().Create(part);

            _viewModel.CleanForm();
        }
    }
}
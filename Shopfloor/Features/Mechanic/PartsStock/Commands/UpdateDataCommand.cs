using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using ExcelDataReader;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Commands
{
    internal sealed class UpdateDataCommand : AsyncCommandBase
    {
        private readonly IProvider<Part> _provider;
        private readonly PartsStockListViewModel _viewModel;
        public UpdateDataCommand(PartsStockListViewModel viewModel, IProvider<Part> provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            PartProvider partProvider = (PartProvider)_provider; //That's nasty AF, but...
            await partProvider.StorageUpdate(_viewModel.DataSource);
        }
    }
}
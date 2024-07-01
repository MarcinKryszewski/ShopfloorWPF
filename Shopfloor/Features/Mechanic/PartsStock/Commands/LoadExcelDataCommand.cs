using ExcelDataReader;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Commands
{
    internal sealed class LoadExcelDataCommand : AsyncCommandBase
    {
        private const string _filePath = "Resources/Zapas części.xlsx";
        private readonly PartsStockListViewModel _viewModel;
        public LoadExcelDataCommand(PartsStockListViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            List<Part> parts = await Task.Run(FillList);
            await _viewModel.LoadData(parts);
        }
        private async Task<List<Part>> FillList()
        {
            DataSet dataSet = await LoadExcel();
            List<Part> parts = [];

            foreach (DataRow item in dataSet.Tables["data"]!.Rows)
            {
                if (item is null) continue;
                if (item.ItemArray[0] is DBNull) continue;
                if (item.ItemArray[0] is null) continue;

                double index = (double)(item.ItemArray[0] ?? 0);
                if (index == 0) continue;

                string? namePl = (item.ItemArray[1] ?? "").ToString();
                string? nameOriginal = (item.ItemArray[10] ?? string.Empty).ToString();
                string? details = (item.ItemArray[3] ?? string.Empty).ToString();
                double storageAmount = (double)(item.ItemArray[2] ?? 0);
                double storageValue = storageAmount == 0 ? 0 : (double)(item.ItemArray[4] ?? 0) / storageAmount;

                Part part = new()
                {
                    NamePl = namePl,
                    TypeId = 0,
                    NameOriginal = nameOriginal ?? string.Empty,
                    Index = (int?)index,
                    Details = details,
                    StorageAmount = storageAmount,
                    StorageValue = storageValue
                };

                parts.Add(part);
            }
            return parts;
        }
        private async Task<DataSet> LoadExcel()
        {
            DataSet dataSet;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                dataSet = await Task.Run(() => reader.AsDataSet(configuration: ReaderConfiguration()));
            }
            return dataSet;
        }
        private static ExcelDataSetConfiguration ReaderConfiguration()
        {
            return new()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                    FilterColumn = (rowReader, columnIndex) =>
                    {
                        if (columnIndex > 11) return false;
                        return true;
                    }
                },
                FilterSheet = (tableReader, sheetIndex) => tableReader.Name == "data",
            };
        }
    }
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
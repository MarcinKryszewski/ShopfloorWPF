using ExcelDataReader;
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
        private readonly string _filePath = "Resources/Zapas części.xlsx";
        private readonly PlannistDashboardMainViewModel _viewModel;
        public LoadExcelDataCommand(PlannistDashboardMainViewModel plannistDashboardMainViewModel)
        {
            _viewModel = plannistDashboardMainViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            List<Part> parts = [];
            ExcelDataSetConfiguration config = new()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            };
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = await Task.Run(() => reader.AsDataSet(configuration: config));

                    foreach (DataRow item in dataSet.Tables["data"]!.Rows)
                    {

                        if (item is null) continue;
                        if (item.ItemArray[0] is DBNull) continue;
                        if (item.ItemArray[0] is null) continue;
                        double index = (double)(item.ItemArray[0] ?? 0);
                        if (index == 0) continue;

                        int indexValue = Convert.ToInt32(index);
                        Part part = new(
                            (item.ItemArray[1] ?? string.Empty).ToString(),
                            (item.ItemArray[10] ?? string.Empty).ToString(),
                            null,
                            indexValue,
                            (item.ItemArray[2] ?? string.Empty).ToString(),
                            null,
                            null,
                            null,
                            (item.ItemArray[3] ?? string.Empty).ToString());
                        parts.Add(part);
                    }
                }
                await _viewModel.LoadData(parts);
            }
        }
    }

    internal sealed class NextPageCommand : CommandBase
    {
        private readonly PaginatedFilterableList _displayList;
        public NextPageCommand(PaginatedFilterableList displayList)
        {
            _displayList = displayList;
        }
        public override void Execute(object? parameter)
        {
            _displayList.PageNext();
        }
    }

    internal sealed class PreviousPageCommand : CommandBase
    {
        private readonly PaginatedFilterableList _displayList;
        public PreviousPageCommand(PaginatedFilterableList displayList)
        {
            _displayList = displayList;
        }
        public override void Execute(object? parameter)
        {
            _displayList.PagePrev();
        }
    }
}
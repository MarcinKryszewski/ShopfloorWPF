using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ExcelDataReader;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Commands
{
    internal sealed class LoadExcelDataCommand : AsyncCommandBase
    {
        private string _filePath = @"C:\Users\kryszm02\Downloads\Zapas części.xlsx";
        private PlannistDashboardMainViewModel _viewModel;

        public LoadExcelDataCommand(PlannistDashboardMainViewModel plannistDashboardMainViewModel)
        {
            _viewModel = plannistDashboardMainViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
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
                        if ((double)item.ItemArray[0] == 0) continue;

                        double val = 0;
                        if (item.ItemArray[0] is not DBNull) val = (double)item.ItemArray[0];
                        Part part = new(item.ItemArray[1].ToString(), item.ItemArray[10].ToString(), null, Convert.ToInt32(val), item.ItemArray[2].ToString(), null, null, null, item.ItemArray[3].ToString());
                        parts.Add(part);
                    }
                }
                _ = _viewModel.LoadData(parts);
                System.Diagnostics.Debug.WriteLine(DateTime.Now);
            }
        }
    }
}
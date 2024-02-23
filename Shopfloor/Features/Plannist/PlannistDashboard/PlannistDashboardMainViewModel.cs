using Shopfloor.Features.Plannist.PlannistDashboard.Commands;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Shopfloor.Features.Plannist.PlannistDashboard
{
    internal sealed class PlannistDashboardMainViewModel : ViewModelBase
    {
        private ObservableCollection<Part> _data = [];
        public ICollectionView Data => CollectionViewSource.GetDefaultView(_data);
        public ICommand LoadExcel { get; }
        public PlannistDashboardMainViewModel(IServiceProvider mainServices)
        {
            LoadExcel = new LoadExcelDataCommand(this);
        }
        public async Task LoadData(List<Part> parts)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);

            int batchSize = 100;
            int totalParts = parts.Count;
            int processedParts = 0;

            while (processedParts < totalParts)
            {
                var batch = parts.Skip(processedParts).Take(batchSize).ToList();

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (Part part in batch)
                    {
                        _data.Add(part);
                    }
                });

                processedParts += batch.Count;

                // Add a delay between batches if needed
                await Task.Delay(500); // Adjust as needed
            }
        }
        public void ClearData()
        {
            _data.Clear();
            Data.Refresh();
        }
    }
}
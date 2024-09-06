using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.God.Commands;
using Shopfloor.Models;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.God
{
    internal sealed class GodViewModel : ViewModelBase
    {
        private readonly List<WorkOrderModel> _workOrders = [];
        private readonly List<PartModel> _parts = [];
        private readonly List<LineModel> _lines = [];
        // private readonly INotifier _notifier;
        public GodViewModel(INotifier notifier)
        {
            _ = LoadDataAsync();
            WorkOrderCreate = new CreateWorkOrderCommand(AddWorkOrder, notifier);
            // _notifier = notifier;
        }
        public WorkOrderDto WorkOrderDto { get; set; } = new();
        public ICommand WorkOrderCreate { get; }
        public ICollectionView WorkOrders => CollectionViewSource.GetDefaultView(_workOrders);
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public async Task AddWorkOrder(WorkOrderModel workOrder)
        {
            _workOrders.Add(workOrder);
            await Application.Current.Dispatcher.InvokeAsync(WorkOrders.Refresh);
            WorkOrderDto = new();
            OnPropertyChanged(nameof(WorkOrderDto));
        }
        private static List<WorkOrderModel> TestDataWorkOrders()
        {
            LineModel line = new() { Id = 1, Name = "Line1" };
            List<WorkOrderModel> testData = [];
            testData.Add(new WorkOrderModel() { Id = 1, Description = "WorkOrder1", Line = line });
            testData.Add(new WorkOrderModel() { Id = 2, Description = "WorkOrder2", Line = line });
            testData.Add(new WorkOrderModel() { Id = 3, Description = "WorkOrder3", Line = line });
            testData.Add(new WorkOrderModel() { Id = 4, Description = "WorkOrder4", Line = line });
            testData.Add(new WorkOrderModel() { Id = 5, Description = "WorkOrder5", Line = line });
            testData.Add(new WorkOrderModel() { Id = 6, Description = "WorkOrder6", Line = line });
            testData.Add(new WorkOrderModel() { Id = 7, Description = "WorkOrder7", Line = line });
            testData.Add(new WorkOrderModel() { Id = 8, Description = "WorkOrder8", Line = line });
            testData.Add(new WorkOrderModel() { Id = 9, Description = "WorkOrder9", Line = line });
            testData.Add(new WorkOrderModel() { Id = 10, Description = "WorkOrder10", Line = line });
            testData.Add(new WorkOrderModel() { Id = 11, Description = "WorkOrder11", Line = line });
            testData.Add(new WorkOrderModel() { Id = 12, Description = "WorkOrder12", Line = line });
            testData.Add(new WorkOrderModel() { Id = 13, Description = "WorkOrder13", Line = line });
            testData.Add(new WorkOrderModel() { Id = 14, Description = "WorkOrder14", Line = line });
            testData.Add(new WorkOrderModel() { Id = 15, Description = "WorkOrder15", Line = line });
            testData.Add(new WorkOrderModel() { Id = 16, Description = "WorkOrder16", Line = line });
            testData.Add(new WorkOrderModel() { Id = 17, Description = "WorkOrder17", Line = line });
            testData.Add(new WorkOrderModel() { Id = 18, Description = "WorkOrder18", Line = line });
            testData.Add(new WorkOrderModel() { Id = 19, Description = "WorkOrder19", Line = line });
            testData.Add(new WorkOrderModel() { Id = 20, Description = "WorkOrder20", Line = line });
            testData.Add(new WorkOrderModel() { Id = 21, Description = "WorkOrder21", Line = line });
            testData.Add(new WorkOrderModel() { Id = 22, Description = "WorkOrder22", Line = line });
            return testData;
        }
        private static List<LineModel> TestDataLines()
        {
            List<LineModel> testData = [];

            testData.Add(new LineModel() { Id = 1, Name = "Line1" });
            testData.Add(new LineModel() { Id = 2, Name = "Line2" });
            testData.Add(new LineModel() { Id = 3, Name = "Line3" });
            testData.Add(new LineModel() { Id = 4, Name = "Line4" });
            testData.Add(new LineModel() { Id = 5, Name = "Line5" });

            return testData;
        }
        private static List<PartModel> TestDataParts()
        {
            List<PartModel> testData = [];
            testData.Add(new PartModel() { Id = 2, Name = "Part2" });
            testData.Add(new PartModel() { Id = 3, Name = "Part3" });
            testData.Add(new PartModel() { Id = 4, Name = "Part4" });
            testData.Add(new PartModel() { Id = 5, Name = "Part5" });
            testData.Add(new PartModel() { Id = 6, Name = "Part6" });
            testData.Add(new PartModel() { Id = 7, Name = "Part7" });
            testData.Add(new PartModel() { Id = 8, Name = "Part8" });
            testData.Add(new PartModel() { Id = 9, Name = "Part9" });
            testData.Add(new PartModel() { Id = 10, Name = "Part10" });
            testData.Add(new PartModel() { Id = 11, Name = "Part11" });
            testData.Add(new PartModel() { Id = 12, Name = "Part12" });
            testData.Add(new PartModel() { Id = 13, Name = "Part13" });
            testData.Add(new PartModel() { Id = 14, Name = "Part14" });
            testData.Add(new PartModel() { Id = 15, Name = "Part15" });
            testData.Add(new PartModel() { Id = 16, Name = "Part16" });
            testData.Add(new PartModel() { Id = 17, Name = "Part17" });
            testData.Add(new PartModel() { Id = 18, Name = "Part18" });
            testData.Add(new PartModel() { Id = 19, Name = "Part19" });
            testData.Add(new PartModel() { Id = 20, Name = "Part20" });
            testData.Add(new PartModel() { Id = 21, Name = "Part21" });
            return testData;
        }
        private async Task LoadDataAsync()
        {
            ClearLists();

            IEnumerable<WorkOrderModel> dataWorkOrder = TestDataWorkOrders();
            IEnumerable<PartModel> dataPart = TestDataParts();
            IEnumerable<LineModel> dataLine = TestDataLines();

            List<Task> tasks = [];
            tasks.Add(BatchListUpdater.UpdateAsync(
                dataWorkOrder,
                _workOrders,
                WorkOrders));

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataPart,
                _parts,
                Parts));

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataLine,
                _lines,
                Lines));

            await Task.WhenAll(tasks);
        }

        private void ClearLists()
        {
            _workOrders.Clear();
            _parts.Clear();
        }
    }
}
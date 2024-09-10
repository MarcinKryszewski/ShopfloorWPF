using System;
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
            List<WorkOrderModel> testData = [
                new WorkOrderModel { Id = 1, Description = "Montaż silnika", LineId = 1, CreateDate = new DateTime(2023, 9, 1) },
                new WorkOrderModel { Id = 2, Description = "Inspekcja podwozia", LineId = 2, CreateDate = new DateTime(2023, 9, 2) },
                new WorkOrderModel { Id = 3, Description = "Remont skrzyni biegów", LineId = 3, CreateDate = new DateTime(2023, 9, 3) },
                new WorkOrderModel { Id = 4, Description = "Lakierowanie karoserii", LineId = 1, CreateDate = new DateTime(2023, 9, 4) },
                new WorkOrderModel { Id = 5, Description = "Kontrola systemu elektrycznego", LineId = 4, CreateDate = new DateTime(2023, 9, 5) },
                new WorkOrderModel { Id = 6, Description = "Instalacja hamulców", LineId = 2, CreateDate = new DateTime(2023, 9, 6) },
                new WorkOrderModel { Id = 7, Description = "Testowanie kontroli jakości", LineId = 5, CreateDate = new DateTime(2023, 9, 7) },
                new WorkOrderModel { Id = 8, Description = "Ustawienie kół", LineId = 3, CreateDate = new DateTime(2023, 9, 8) },
                new WorkOrderModel { Id = 9, Description = "Montaż wnętrza", LineId = 4, CreateDate = new DateTime(2023, 9, 9) },
                new WorkOrderModel { Id = 10, Description = "Kalibracja układu paliwowego", LineId = 1, CreateDate = new DateTime(2023, 9, 10) },
                new WorkOrderModel { Id = 11, Description = "Regulacja zawieszenia", LineId = 2, CreateDate = new DateTime(2023, 9, 11) },
                new WorkOrderModel { Id = 12, Description = "Instalacja układu wydechowego", LineId = 5, CreateDate = new DateTime(2023, 9, 12) },
                new WorkOrderModel { Id = 13, Description = "Montaż drzwi", LineId = 3, CreateDate = new DateTime(2023, 9, 13) },
                new WorkOrderModel { Id = 14, Description = "Wymiana przedniej szyby", LineId = 4, CreateDate = new DateTime(2023, 9, 14) },
                new WorkOrderModel { Id = 15, Description = "Integracja akumulatora", LineId = 1, CreateDate = new DateTime(2023, 9, 15) },
                new WorkOrderModel { Id = 16, Description = "Regulacja świateł", LineId = 2, CreateDate = new DateTime(2023, 9, 16) },
                new WorkOrderModel { Id = 17, Description = "Montaż układu chłodzenia", LineId = 3, CreateDate = new DateTime(2023, 9, 17) },
                new WorkOrderModel { Id = 18, Description = "Montaż foteli", LineId = 5, CreateDate = new DateTime(2023, 9, 18) },
                new WorkOrderModel { Id = 19, Description = "Konfiguracja deski rozdzielczej", LineId = 4, CreateDate = new DateTime(2023, 9, 19) },
                new WorkOrderModel { Id = 20, Description = "Końcowa inspekcja pojazdu", LineId = 1, CreateDate = new DateTime(2023, 9, 20) }
            ];

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
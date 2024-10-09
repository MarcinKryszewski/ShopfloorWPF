using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderRepository : IRepository<WorkOrderModel, WorkOrderCreationModel>
    {
        private readonly IStore<WorkOrderModel> _store;
        private bool _dataLoaded = false;
        public WorkOrderRepository(IStore<WorkOrderModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<WorkOrderModel> Create(WorkOrderCreationModel item)
        {
            Random rnd = new();
            WorkOrderModel workOrder = item.CreateModel(rnd.Next(0, 100));
            await _store.AddItem(workOrder);
            return workOrder;
        }

        public async Task Delete(int id)
        {
            // TODO: Remove in DB
            WorkOrderModel? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                string errorText = "Nie udało się anulować tego działania. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            _store.Data.Remove(item);
        }

        public async Task<List<WorkOrderModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                // TODO: Get data from provider
                List<WorkOrderModel> data = [
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
                new WorkOrderModel { Id = 20, Description = "Końcowa inspekcja pojazdu", LineId = 1, CreateDate = new DateTime(2023, 9, 20) },
                ];

                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data.Where(x => !x.IsDeleted).ToList();
        }
        public async Task Update(WorkOrderCreationModel item)
        {
            WorkOrderModel? workOrder = _store.Data.Find(x => x.Id == item.Id);
            if (workOrder == null)
            {
                string errorText = "Nie udało się zmienić tego działania. Popraw dane i spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            workOrder.LineId = item.LineId;
            workOrder.Description = item.Description;
            workOrder.Line = item.Line;

            workOrder.Parts.Clear();
            workOrder.Parts.AddRange(item.Parts);

            workOrder.PartsId.Clear();
            workOrder.PartsId.AddRange(item.PartsId);

            await Task.Delay(0);
        }
    }
}
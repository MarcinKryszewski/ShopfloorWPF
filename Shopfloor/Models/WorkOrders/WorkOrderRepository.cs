using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderRepository : IRepository<WorkOrderModel, WorkOrderCreationModel>
    {
        public Task<WorkOrderModel> Create(WorkOrderCreationModel item)
        {
            Random rnd = new();

            WorkOrderModel workOrder = new()
            {
                Id = rnd.Next(0, 100),
                CreateDate = DateTime.Now,
                LineId = item.LineId,
                Description = item.Description,
                Line = item.Line,
                Parts = item.Parts,
                PartsId = item.PartsId,
            };
            return Task.FromResult(workOrder);
        }

        public Task<WorkOrderModel> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkOrderModel>> GetData()
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
                new WorkOrderModel { Id = 20, Description = "Końcowa inspekcja pojazdu", LineId = 1, CreateDate = new DateTime(2023, 9, 20) },
            ];

            await Task.Delay(0);
            return testData;
        }
        public Task<WorkOrderModel> Update()
        {
            throw new NotImplementedException();
        }
    }
}
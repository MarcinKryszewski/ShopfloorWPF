using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderStore : IStore<WorkOrderModel>
    {
        private readonly IRepository<WorkOrderModel> _repository;
        private List<WorkOrderModel> _data = [];
        private bool _dataLoaded = false;
        public WorkOrderStore(IRepository<WorkOrderModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<WorkOrderModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                _data = await LoadData();
            }
            return _data;
        }

        public async Task ReloadData()
        {
            List<WorkOrderModel> testData = [
                new WorkOrderModel { Id = 1, Description = "Montaż systemu klimatyzacji", LineId = 1, CreateDate = new DateTime(2023, 9, 1) },
                new WorkOrderModel { Id = 2, Description = "Spawanie konstrukcji", LineId = 2, CreateDate = new DateTime(2023, 9, 2) },
                new WorkOrderModel { Id = 3, Description = "Test układu hamulcowego", LineId = 3, CreateDate = new DateTime(2023, 9, 3) },
                new WorkOrderModel { Id = 4, Description = "Instalacja układu kierowniczego", LineId = 1, CreateDate = new DateTime(2023, 9, 4) },
                new WorkOrderModel { Id = 5, Description = "Konserwacja układu wydechowego", LineId = 4, CreateDate = new DateTime(2023, 9, 5) },
                new WorkOrderModel { Id = 6, Description = "Montaż skrzyni biegów", LineId = 2, CreateDate = new DateTime(2023, 9, 6) },
                new WorkOrderModel { Id = 7, Description = "Test bezpieczeństwa pojazdu", LineId = 5, CreateDate = new DateTime(2023, 9, 7) },
                new WorkOrderModel { Id = 8, Description = "Wymiana opon", LineId = 3, CreateDate = new DateTime(2023, 9, 8) },
                new WorkOrderModel { Id = 9, Description = "Malowanie felg", LineId = 4, CreateDate = new DateTime(2023, 9, 9) },
                new WorkOrderModel { Id = 10, Description = "Montaż systemu multimedialnego", LineId = 1, CreateDate = new DateTime(2023, 9, 10) },
                new WorkOrderModel { Id = 11, Description = "Kalibracja systemu nawigacji", LineId = 2, CreateDate = new DateTime(2023, 9, 11) },
                new WorkOrderModel { Id = 12, Description = "Instalacja poduszek powietrznych", LineId = 5, CreateDate = new DateTime(2023, 9, 12) },
                new WorkOrderModel { Id = 13, Description = "Przegląd układu chłodzenia", LineId = 3, CreateDate = new DateTime(2023, 9, 13) },
                new WorkOrderModel { Id = 14, Description = "Kontrola jakości silnika", LineId = 4, CreateDate = new DateTime(2023, 9, 14) },
                new WorkOrderModel { Id = 15, Description = "Montaż bocznych luster", LineId = 1, CreateDate = new DateTime(2023, 9, 15) },
                new WorkOrderModel { Id = 16, Description = "Przegląd techniczny pojazdu", LineId = 2, CreateDate = new DateTime(2023, 9, 16) },
                new WorkOrderModel { Id = 17, Description = "Montaż reflektorów", LineId = 3, CreateDate = new DateTime(2023, 9, 17) },
                new WorkOrderModel { Id = 18, Description = "Konfiguracja automatycznej skrzyni biegów", LineId = 5, CreateDate = new DateTime(2023, 9, 18) },
                new WorkOrderModel { Id = 19, Description = "Naprawa układu paliwowego", LineId = 4, CreateDate = new DateTime(2023, 9, 19) },
                new WorkOrderModel { Id = 20, Description = "Kontrola szczelności zbiornika paliwa", LineId = 1, CreateDate = new DateTime(2023, 9, 20) }
            ];

            await Task.Delay(0);
            _data.Clear();
            _data.AddRange(testData);
        }
        private async Task<List<WorkOrderModel>> LoadData()
        {
            List<WorkOrderModel> data = await _repository.GetData();
            _dataLoaded = true;
            return data;
        }
    }
}
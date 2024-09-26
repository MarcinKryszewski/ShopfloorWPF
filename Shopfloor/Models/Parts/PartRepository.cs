using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Parts
{
    internal class PartRepository : IRepository<PartModel, PartCreationModel>
    {
        private readonly IStore<PartModel> _store;
        private bool _dataLoaded = false;
        public PartRepository(IStore<PartModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];
        public Task<PartModel> Create(PartCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<PartModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<PartModel> data = [
                    new PartModel { Id = 1, Amount = 42.5, ManufacturerId = 1, PartTypeId = 3, NameOriginal = "Engine Block", NamePl = "Blok Silnika", Index = "EB-101", IsConfirmed = true },
                    new PartModel { Id = 2, Amount = 18.3, ManufacturerId = 2, PartTypeId = 5, NameOriginal = "Oil Filter", NamePl = "Filtr Oleju", Index = "OF-102", IsConfirmed = true },
                    new PartModel { Id = 3, Amount = 60.0, ManufacturerId = 3, PartTypeId = 2, NameOriginal = "Air Filter", NamePl = "Filtr Powietrza", Index = "AF-103", IsConfirmed = true },
                    new PartModel { Id = 4, Amount = 22.7, ManufacturerId = 4, PartTypeId = 1, NameOriginal = "Brake Pad", NamePl = "Klocki Hamulcowe", Index = "BP-104", IsConfirmed = true },
                    new PartModel { Id = 5, Amount = 75.2, ManufacturerId = 5, PartTypeId = 6, NameOriginal = "Suspension Spring", NamePl = "Sprężyna Zawieszenia", Index = "SS-105", IsConfirmed = false },
                    new PartModel { Id = 6, Amount = 34.9, ManufacturerId = 1, PartTypeId = 4, NameOriginal = "Clutch Disc", NamePl = "Tarcza Sprzęgła", Index = "CD-106", IsConfirmed = true },
                    new PartModel { Id = 7, Amount = 99.1, ManufacturerId = 2, PartTypeId = 7, NameOriginal = "Gearbox", NamePl = "Skrzynia Biegów", Index = "GB-107", IsConfirmed = true },
                    new PartModel { Id = 8, Amount = 47.3, ManufacturerId = 3, PartTypeId = 3, NameOriginal = "Radiator", NamePl = "Chłodnica", Index = "RD-108", IsConfirmed = true },
                    new PartModel { Id = 9, Amount = 10.4, ManufacturerId = 4, PartTypeId = 6, NameOriginal = "Water Pump", NamePl = "Pompa Wody", Index = "WP-109", IsConfirmed = true },
                    new PartModel { Id = 10, Amount = 80.8, ManufacturerId = 5, PartTypeId = 4, NameOriginal = "Alternator", NamePl = "Alternator", Index = "AL-110", IsConfirmed = true },
                    new PartModel { Id = 11, Amount = 23.6, ManufacturerId = 1, PartTypeId = 2, NameOriginal = "Fuel Pump", NamePl = "Pompa Paliwa", Index = "FP-111", IsConfirmed = true },
                    new PartModel { Id = 12, Amount = 90.7, ManufacturerId = 2, PartTypeId = 1, NameOriginal = "Ignition Coil", NamePl = "Cewka Zapłonowa", Index = "IC-112", IsConfirmed = false },
                    new PartModel { Id = 13, Amount = 15.9, ManufacturerId = 3, PartTypeId = 5, NameOriginal = "Turbocharger", NamePl = "Turbosprężarka", Index = "TC-113", IsConfirmed = true },
                    new PartModel { Id = 14, Amount = 67.4, ManufacturerId = 4, PartTypeId = 3, NameOriginal = "Fuel Injector", NamePl = "Wtryskiwacz Paliwa", Index = "FI-114", IsConfirmed = true },
                    new PartModel { Id = 15, Amount = 48.5, ManufacturerId = 5, PartTypeId = 7, NameOriginal = "Exhaust Manifold", NamePl = "Kolektor Wydechowy", Index = "EM-115", IsConfirmed = true },
                    new PartModel { Id = 16, Amount = 35.0, ManufacturerId = 1, PartTypeId = 6, NameOriginal = "Timing Belt", NamePl = "Pasek Rozrządu", Index = "TB-116", IsConfirmed = false },
                    new PartModel { Id = 17, Amount = 28.2, ManufacturerId = 2, PartTypeId = 4, NameOriginal = "Crankshaft", NamePl = "Wał Korbowy", Index = "CS-117", IsConfirmed = true },
                    new PartModel { Id = 18, Amount = 53.1, ManufacturerId = 3, PartTypeId = 2, NameOriginal = "Piston", NamePl = "Tłok", Index = "PI-118", IsConfirmed = true },
                    new PartModel { Id = 19, Amount = 72.9, ManufacturerId = 4, PartTypeId = 1, NameOriginal = "Camshaft", NamePl = "Wałek Rozrządu", Index = "CA-119", IsConfirmed = true },
                    new PartModel { Id = 20, Amount = 55.4, ManufacturerId = 5, PartTypeId = 5, NameOriginal = "Drive Shaft", NamePl = "Wał Napędowy", Index = "DS-120", IsConfirmed = true },
                ];

                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }
        public Task Update(PartCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PartTypes
{
    internal class PartTypeRepository : IRepository<PartTypeModel, PartTypeCreationModel>
    {
        private readonly IStore<PartTypeModel> _store;
        private bool _dataLoaded = false;
        public PartTypeRepository(IStore<PartTypeModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];
        public Task<PartTypeModel> Create(PartTypeCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<PartTypeModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<PartTypeModel> data = [
                    new PartTypeModel { Id = 1, Name = "Bearing" },
                    new PartTypeModel { Id = 2, Name = "Exhaust" },
                    new PartTypeModel { Id = 3, Name = "Brake Disc" },
                    new PartTypeModel { Id = 4, Name = "Oil Filter" },
                    new PartTypeModel { Id = 5, Name = "Fuel Pump" },
                    new PartTypeModel { Id = 6, Name = "Timing Belt" },
                    new PartTypeModel { Id = 7, Name = "Spark Plug" },
                ];

                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }
        public Task Update(PartTypeCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}
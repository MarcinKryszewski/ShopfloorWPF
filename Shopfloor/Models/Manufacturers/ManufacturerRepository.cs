using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Manufacturers
{
    internal class ManufacturerRepository : IRepository<ManufacturerModel, ManufacturerCreationModel>
    {
        private readonly IStore<ManufacturerModel> _store;
        private bool _dataLoaded = false;
        public ManufacturerRepository(IStore<ManufacturerModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];

        public Task<ManufacturerModel> Create(ManufacturerCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ManufacturerModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<ManufacturerModel> data = [
                    new ManufacturerModel { Id = 1, Name = "Bosch" },
                    new ManufacturerModel { Id = 2, Name = "Delphi" },
                    new ManufacturerModel { Id = 3, Name = "Magneti Marelli" },
                    new ManufacturerModel { Id = 4, Name = "Valeo" },
                    new ManufacturerModel { Id = 5, Name = "Denso" },
                ];

                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }
        public Task Update(ManufacturerCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}
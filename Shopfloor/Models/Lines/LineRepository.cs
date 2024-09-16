using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineRepository : IRepository<LineModel, LineCreationModel>
    {
        private readonly IStore<LineModel> _store;
        private bool _dataLoaded = false;
        public LineRepository(IStore<LineModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];
        public Task<LineModel> Create(LineCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LineModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                // TODO: Get data from provider
                List<LineModel> data = [
                new LineModel { Id = 1, Name = "Linia Montażowa Silników" },
                new LineModel { Id = 2, Name = "Linia Spawania Podwozi" },
                new LineModel { Id = 3, Name = "Linia Lakierowania Karoserii" },
                new LineModel { Id = 4, Name = "Linia Montażu Układów Elektrycznych" },
                new LineModel { Id = 5, Name = "Linia Testów Bezpieczeństwa" }
                ];
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }

        public Task Update(LineCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}
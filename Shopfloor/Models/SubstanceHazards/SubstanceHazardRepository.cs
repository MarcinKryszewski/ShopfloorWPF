using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SubstanceHazards
{
    internal class SubstanceHazardRepository : IRepository<SubstanceHazard, SubstanceHazardCreation>
    {
        private readonly IProvider<SubstanceHazard, SubstanceHazardCreation> _provider;
        private readonly IStore<SubstanceHazard> _store;
        private bool _dataLoaded = false;
        private bool _loading = false;
        public SubstanceHazardRepository(
            IStore<SubstanceHazard> store,
            IProvider<SubstanceHazard,
            SubstanceHazardCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<SubstanceHazard> Create(SubstanceHazardCreation item)
        {
            int id = await _provider.Create(item);
            SubstanceHazard model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            SubstanceHazard? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _provider.Delete(id);
                _store.Data.Remove(item);
            }
            catch (Exception)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<List<SubstanceHazard>> GetDataAsync()
        {
            while (_loading)
            {
                await Task.Delay(3);
            }
            if (!_dataLoaded)
            {
                _loading = true;
                _dataLoaded = true;
                List<SubstanceHazard> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _loading = false;
            }

            return _store.Data;
        }
        public async Task Update(SubstanceHazardCreation item)
        {
            SubstanceHazard? existingData = _store.Data.Find(x => x.Id == item.Id);

            if (existingData is null)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _provider.Update(existingData);
            existingData.SetValues(item);

            await Task.CompletedTask;
        }
    }
}
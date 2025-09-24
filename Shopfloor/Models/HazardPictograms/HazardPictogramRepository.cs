using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardPictograms
{
    internal class HazardPictogramRepository : IRepository<HazardPictogram, HazardPictogramCreation>
    {
        private readonly IProvider<HazardPictogram, HazardPictogramCreation> _provider;
        private readonly IStore<HazardPictogram> _store;
        private bool _dataLoaded = false;
        private bool _loading = false;
        public HazardPictogramRepository(
            IStore<HazardPictogram> store,
            IProvider<HazardPictogram,
            HazardPictogramCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<HazardPictogram> Create(HazardPictogramCreation item)
        {
            int id = await _provider.Create(item);
            HazardPictogram model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            HazardPictogram? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<HazardPictogram>> GetDataAsync()
        {
            while (_loading)
            {
                await Task.Delay(3);
            }
            if (!_dataLoaded)
            {
                _loading = true;
                _dataLoaded = true;
                List<HazardPictogram> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _loading = false;
            }

            return _store.Data;
        }
        public async Task Update(HazardPictogramCreation item)
        {
            HazardPictogram? existingData = _store.Data.Find(x => x.Id == item.Id);

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
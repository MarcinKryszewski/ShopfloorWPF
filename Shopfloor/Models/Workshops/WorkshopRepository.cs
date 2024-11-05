using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class WorkshopRepository : IRepository<Workshop, WorkshopCreation>
    {
        private readonly IProvider<Workshop, WorkshopCreation> _provider;
        private readonly IStore<Workshop> _store;
        private bool _dataLoaded = false;
        public WorkshopRepository(IStore<Workshop> store, IProvider<Workshop, WorkshopCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<Workshop> Create(WorkshopCreation item)
        {
            int id = await _provider.Create(item);
            Workshop model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            Workshop? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<Workshop>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<Workshop> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(WorkshopCreation item)
        {
            Workshop? existingData = _store.Data.Find(x => x.Id == item.Id);

            if (existingData is null)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _provider.Update(existingData);
            // existingData.SetValues(item);

            await Task.CompletedTask;
        }
    }
}
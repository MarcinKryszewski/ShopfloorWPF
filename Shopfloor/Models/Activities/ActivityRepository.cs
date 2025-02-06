using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Activities
{
    internal class ActivityRepository : IRepository<Activity, ActivityCreation>
    {
        private readonly IProvider<Activity, ActivityCreation> _provider;
        private readonly IStore<Activity> _store;
        private bool _dataLoaded = false;
        public ActivityRepository(IStore<Activity> store, IProvider<Activity, ActivityCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<Activity> Create(ActivityCreation item)
        {
            int id = await _provider.Create(item);
            Activity model = ActivityMapper.ToModel(id, item);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            Activity? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<Activity>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<Activity> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(ActivityCreation item)
        {
            Activity? existingData = _store.Data.Find(x => x.Id == item.Id);

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
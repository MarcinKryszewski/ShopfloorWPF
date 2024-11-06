using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivityTypes
{
    internal class ActivityTypeRepository : IRepository<ActivityType, ActivityTypeCreation>
    {
        private readonly IProvider<ActivityType, ActivityTypeCreation> _provider;
        private readonly IStore<ActivityType> _store;
        private bool _dataLoaded = false;
        public ActivityTypeRepository(
            IStore<ActivityType> store,
            IProvider<ActivityType,
            ActivityTypeCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<ActivityType> Create(ActivityTypeCreation item)
        {
            int id = await _provider.Create(item);
            ActivityType model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            ActivityType? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<ActivityType>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<ActivityType> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(ActivityTypeCreation item)
        {
            ActivityType? existingData = _store.Data.Find(x => x.Id == item.Id);

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
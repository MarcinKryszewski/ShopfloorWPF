using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivitiesInstructions
{
    internal class ActivityInstructionRepository : IRepository<ActivityInstruction, ActivityInstructionCreation>
    {
        private readonly IProvider<ActivityInstruction, ActivityInstructionCreation> _provider;
        private readonly IStore<ActivityInstruction> _store;
        private bool _dataLoaded = false;
        public ActivityInstructionRepository(IStore<ActivityInstruction> store, IProvider<ActivityInstruction, ActivityInstructionCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<ActivityInstruction> Create(ActivityInstructionCreation item)
        {
            int id = await _provider.Create(item);
            ActivityInstruction model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            ActivityInstruction? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<ActivityInstruction>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<ActivityInstruction> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(ActivityInstructionCreation item)
        {
            ActivityInstruction? existingData = _store.Data.Find(x => x.Id == item.Id);

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
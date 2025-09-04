using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkshopSubstances
{
    internal class WorkshopSubstanceRepository : IRepository<WorkshopSubstance, WorkshopSubstanceCreation>
    {
        private readonly IProvider<WorkshopSubstance, WorkshopSubstanceCreation> _provider;
        private readonly IStore<WorkshopSubstance> _store;
        private bool _dataLoaded = false;
        public WorkshopSubstanceRepository(IStore<WorkshopSubstance> store, IProvider<WorkshopSubstance, WorkshopSubstanceCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<WorkshopSubstance> Create(WorkshopSubstanceCreation item)
        {
            int id = await _provider.Create(item);
            WorkshopSubstance model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            WorkshopSubstance? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<WorkshopSubstance>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<WorkshopSubstance> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(WorkshopSubstanceCreation item)
        {
            WorkshopSubstance? existingData = _store.Data.Find(x => x.Id == item.Id);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HPhrases
{
    internal class HPhraseRepository : IRepository<HPhrase, HPhraseCreation>
    {
        private readonly IProvider<HPhrase, HPhraseCreation> _provider;
        private readonly IStore<HPhrase> _store;
        private bool _dataLoaded = false;
        public HPhraseRepository(IStore<HPhrase> store, IProvider<HPhrase, HPhraseCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<HPhrase> Create(HPhraseCreation item)
        {
            int id = await _provider.Create(item);
            HPhrase model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            HPhrase? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<HPhrase>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<HPhrase> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(HPhraseCreation item)
        {
            HPhrase? existingData = _store.Data.Find(x => x.Id == item.Id);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.RiskEvaluations
{
    internal class RiskEvaluationRepository : IRepository<RiskEvaluation, RiskEvaluationCreation>
    {
        private readonly IProvider<RiskEvaluation, RiskEvaluationCreation> _provider;
        private readonly IStore<RiskEvaluation> _store;
        private bool _dataLoaded = false;
        private bool _loading = false;
        public RiskEvaluationRepository(
            IStore<RiskEvaluation> store,
            IProvider<RiskEvaluation,
            RiskEvaluationCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<RiskEvaluation> Create(RiskEvaluationCreation item)
        {
            int id = await _provider.Create(item);
            RiskEvaluation model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            RiskEvaluation? item = _store.Data.Find(x => x.Id == id);
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
        public async Task<List<RiskEvaluation>> GetDataAsync()
        {
            while (_loading)
            {
                await Task.Delay(3);
            }
            if (!_dataLoaded)
            {
                _loading = true;
                _dataLoaded = true;
                List<RiskEvaluation> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _loading = false;
            }

            return _store.Data;
        }
        public async Task Update(RiskEvaluationCreation item)
        {
            RiskEvaluation? existingData = _store.Data.Find(x => x.Id == item.Id);

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
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store.Combine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store
{
    internal sealed class ErrandStore : IDataStore<Errand>
    {
        private List<Errand> _data = [];
        private readonly ErrandProvider _provider;
        private readonly ErrandCombine _combiner;
        public ErrandStore(ErrandProvider provider, ErrandCombine combiner)
        {
            _provider = provider;
            _combiner = combiner;
        }
        public List<Errand> GetData(bool shouldCombine = false)
        {
            if (!IsLoaded) Load();
            if (shouldCombine) _combiner.Combine().Wait();
            return _data;
        }
        public bool IsLoaded { get; private set; }
        private Task Load()
        {
            _data = new(_provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            _data = new(await _provider.GetAll());
        }
    }
}
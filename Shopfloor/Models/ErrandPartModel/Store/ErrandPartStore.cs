using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel.Store.Combine;

namespace Shopfloor.Models.ErrandPartModel.Store
{
    internal sealed class ErrandPartStore : StoreBase<ErrandPart>, IDataStore<ErrandPart>
    {
        //private List<ErrandPart> _data = [];
        //private readonly ErrandPartProvider _provider;
        //private readonly ErrandPartCombiner _combiner;
        public ErrandPartStore(ErrandPartProvider provider, ErrandPartCombiner combiner)
            : base(provider, combiner)
        {
            //_provider = provider;
            //_combiner = combiner;
        }
        /*public List<ErrandPart> GetData(bool shouldCombine = false)
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
        }*/
    }
}
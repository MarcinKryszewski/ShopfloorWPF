using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal class ErrandTypeStore : IDataStore<ErrandType>
    {
        private readonly ErrandTypeProvider _provider;
        private List<ErrandType> _data = [];
        public ErrandTypeStore(ErrandTypeProvider provider)
        {
            _provider = provider;
        }
        public List<ErrandType> GetData(bool shouldCombine = false)
        {
            if (!IsLoaded) Load();
            if (shouldCombine) CombineData();
            return _data;
        }
        public bool IsLoaded { get; private set; } = false;
        public Task CombineData()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
        public Task Load()
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
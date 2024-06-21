using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandStoreService : IErrandStoreService
    {
        private readonly IDataStore<Errand> _errandStore;
        public ErrandStoreService(IDataStore<Errand> errandStore)
        {
            _errandStore = errandStore;
        }
        public void AddErrandToStore(Errand errand) => _errandStore.Data.Add(errand);
    }

    internal interface IErrandStoreService
    {
        public void AddErrandToStore(Errand item);
    }
}
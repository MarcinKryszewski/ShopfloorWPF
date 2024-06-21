using Shopfloor.Models.ErrandModel.Store;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandStoreService
    {
        private readonly ErrandStore _errandStore;
        public ErrandStoreService(ErrandStore errandStore)
        {
            _errandStore = errandStore;
        }
        public void AddErrandToStore(Errand errand) => _errandStore.Data.Add(errand);
    }
}
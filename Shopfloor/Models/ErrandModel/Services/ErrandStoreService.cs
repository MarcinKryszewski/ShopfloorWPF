using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandStoreService : IDataModelStoreService<Errand>
    {
        private readonly IDataStore<Errand> _store;
        public ErrandStoreService(IDataStore<Errand> store)
        {
            _store = store;
        }
        public void AddToStore(Errand item) => _store.Data.Add(item);

        public void EditInStore(Errand item)
        {
            throw new System.NotImplementedException();
        }
    }
}
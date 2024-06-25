using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartStatusModel.Services
{
    internal class ErrandPartStatusStoreService : IDataModelStoreService<ErrandPartStatus>
    {
        private readonly IDataStore<ErrandPartStatus> _store;
        public ErrandPartStatusStoreService(IDataStore<ErrandPartStatus> store)
        {
            _store = store;
        }
        public void AddToStore(ErrandPartStatus item) => _store.Data.Add(item);
    }
}
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
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
            int index = _store.Data.FindIndex(t => t.Id == item.Id);
            if (index != -1) _store.Data[index] = item;
        }

        public Errand? FindItem(Errand item)
        {
            return _store.Data.FirstOrDefault(t => t.Id == item.Id);
        }
    }
}
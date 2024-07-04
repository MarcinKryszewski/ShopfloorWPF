using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandPartStatusModel.Services
{
    internal class ErrandPartStatusStoreService : IDataModelStoreService<ErrandPartStatus>
    {
        private readonly IDataStore<ErrandPartStatus> _store;
        public ErrandPartStatusStoreService(IDataStore<ErrandPartStatus> store)
        {
            _store = store;
        }
        public void Add(ErrandPartStatus item) => _store.Data.Add(item);
        public void Edit(ErrandPartStatus item)
        {
            throw new System.NotImplementedException();
        }
        public ErrandPartStatus? FindItem(ErrandPartStatus item)
        {
            return _store.Data.FirstOrDefault(t => t.Id == item.Id);
        }

        public void Remove(ErrandPartStatus item)
        {
            throw new System.NotImplementedException();
        }
    }
}
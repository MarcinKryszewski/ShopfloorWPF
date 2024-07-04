using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusStoreService : IDataModelStoreService<ErrandStatus>
    {
        private readonly IDataStore<ErrandStatus> _store;
        public ErrandStatusStoreService(IDataStore<ErrandStatus> errandStatusStore)
        {
            _store = errandStatusStore;
        }
        public void Add(ErrandStatus status) => _store.Data.Add(status);
        public void Edit(ErrandStatus status)
        {
            int index = _store.Data.FindIndex(es => es.Id == status.Id);
            if (index != -1) _store.Data[index] = status;
        }
        public ErrandStatus? FindItem(ErrandStatus item) => _store.Data.FirstOrDefault(t => t.Id == item.Id);

        public void Remove(ErrandStatus item)
        {
            throw new System.NotImplementedException();
        }
    }
}
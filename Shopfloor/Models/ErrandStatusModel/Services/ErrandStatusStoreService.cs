using System.Linq;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusStoreService : IDataModelStoreService<ErrandStatus>
    {
        private readonly ErrandStatusStore _store;
        public ErrandStatusStoreService(ErrandStatusStore errandStatusStore)
        {
            _store = errandStatusStore;
        }
        public void AddToStore(ErrandStatus status) => _store.Data.Add(status);
        public void EditInStore(ErrandStatus status)
        {
            int index = _store.Data.FindIndex(es => es.Id == status.Id);
            if (index != -1) _store.Data[index] = status;
        }
    }
}
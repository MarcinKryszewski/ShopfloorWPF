using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusStoreService : IDataModelStoreService<ErrandStatus>
    {
        private readonly ErrandStatusStore _errandStatusStore;
        public ErrandStatusStoreService(ErrandStatusStore errandStatusStore)
        {
            _errandStatusStore = errandStatusStore;
        }
        public void AddToStore(ErrandStatus status) => _errandStatusStore.Data.Add(status);
    }
}
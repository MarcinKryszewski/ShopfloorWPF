using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrandPartStatus : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStatusStore _errandPartStatusStore;
        private readonly ErrandPartStore _errandPartStore;
        public ErrandPartToErrandPartStatus(ErrandPartStatusStore errandPartStatusStore, ErrandPartStore errandPartStore)
        {
            _errandPartStatusStore = errandPartStatusStore;
            _errandPartStore = errandPartStore;
        }
        public Task CombineAll()
        {
            List<ErrandPartStatus> statuses = GetErrandPartStatuses();

            foreach (ErrandPart errandPart in _errandPartStore.Data)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPartStatus> GetErrandPartStatuses() => _errandPartStatusStore.Data;
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
    }
}
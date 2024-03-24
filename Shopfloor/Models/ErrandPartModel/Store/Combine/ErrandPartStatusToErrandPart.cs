using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartStatusToErrandPart : ICombiner
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandPartStatusStore _errandPartStatusStore;
        public ErrandPartStatusToErrandPart(ErrandPartStore errandPartStore, ErrandPartStatusStore errandPartStatusStore)
        {
            _errandPartStore = errandPartStore;
            _errandPartStatusStore = errandPartStatusStore;
        }
        public Task Combine()
        {
            List<ErrandPart> errandParts = GetErrandParts();
            List<ErrandPartStatus> statuses = GetErrandPartStatuses();

            foreach (ErrandPart errandPart in errandParts)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
        private List<ErrandPartStatus> GetErrandPartStatuses() => _errandPartStatusStore.GetData();
    }
}
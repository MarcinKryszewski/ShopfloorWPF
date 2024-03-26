using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrandPartStatus : ICombiner<ErrandPart>
    {
        private readonly IDataStore<ErrandPartStatus> _errandPartStatusStore;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        public ErrandPartToErrandPartStatus(IDataStore<ErrandPartStatus> errandPartStatusStore, IDataStore<ErrandPart> errandPartStore)
        {
            _errandPartStatusStore = errandPartStatusStore;
            _errandPartStore = errandPartStore;
        }
        public Task Combine(List<ErrandPart> data)
        {
            List<ErrandPartStatus> statuses = GetErrandPartStatuses();

            foreach (ErrandPart errandPart in _errandPartStore.GetData())
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPartStatus> GetErrandPartStatuses() => _errandPartStatusStore.GetData();
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
    }
}
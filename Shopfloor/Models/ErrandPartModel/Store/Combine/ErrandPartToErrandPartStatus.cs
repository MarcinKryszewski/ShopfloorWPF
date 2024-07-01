using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
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
        public Task CombineAll()
        {
            List<ErrandPartStatus> statuses = GetErrandPartStatuses();

            foreach (ErrandPart item in _errandPartStore.Data)
            {
                Combine(statuses, item);
            }
            return Task.CompletedTask;
        }

        private static void Combine(List<ErrandPartStatus> statuses, ErrandPart item)
        {
            item.StatusList.Clear();
            item.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == item.Id));
        }

        public Task CombineOne(ErrandPart item)
        {
            List<ErrandPartStatus> statuses = GetErrandPartStatuses();

            Combine(statuses, item);

            return Task.CompletedTask;
        }
        private List<ErrandPartStatus> GetErrandPartStatuses() => _errandPartStatusStore.Data;
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;

    }
}
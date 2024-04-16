﻿using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandStatus : ICombiner<Errand>
    {
        private readonly ErrandStore _errandStore;
        private readonly ErrandStatusStore _errandStatusStore;
        public ErrandToErrandStatus(ErrandStatusStore errandStatusStore, ErrandStore errandStore)
        {
            _errandStatusStore = errandStatusStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<Errand> errands = GetErrands();
            List<ErrandStatus> errandStatuses = LoadErrandStatuses();
            foreach (Errand errand in errands)
            {
                errand.Statuses.Clear();
                errand.Statuses.AddRange(errandStatuses.Where(errandStatus => errandStatus.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandStatus> LoadErrandStatuses() => _errandStatusStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;
    }
}
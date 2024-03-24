﻿using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandStatusToErrand : ICombiner
    {
        private readonly ErrandStore _errandStore;
        private readonly ErrandStatusStore _errandStatusStore;
        public ErrandStatusToErrand(ErrandStatusStore errandStatusStore, ErrandStore errandStore)
        {
            _errandStatusStore = errandStatusStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<ErrandStatus> errandStatuses = LoadErrandStatuses();
            List<Errand> errands = LoadErrands();
            foreach (Errand errand in errands)
            {
                errand.Statuses.Clear();
                errand.Statuses.AddRange(errandStatuses.Where(errandStatus => errandStatus.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<Errand> LoadErrands() => _errandStore.GetData();
        private List<ErrandStatus> LoadErrandStatuses() => _errandStatusStore.GetData();
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandStatusModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandStatus : ICombiner<Errand>
    {
        private readonly IDataStore<ErrandStatus> _errandStatusStore;
        public ErrandToErrandStatus(IDataStore<ErrandStatus> errandStatusStore)
        {
            _errandStatusStore = errandStatusStore;
        }
        public Task Combine(List<Errand> data)
        {
            List<ErrandStatus> errandStatuses = LoadErrandStatuses();
            foreach (Errand errand in data)
            {
                errand.Statuses.Clear();
                errand.Statuses.AddRange(errandStatuses.Where(errandStatus => errandStatus.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandStatus> LoadErrandStatuses() => _errandStatusStore.GetData();
    }
}
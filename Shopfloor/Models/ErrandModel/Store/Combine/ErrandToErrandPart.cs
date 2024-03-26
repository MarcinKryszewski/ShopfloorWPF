using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandPart : ICombiner<Errand>
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        public ErrandToErrandPart(IDataStore<ErrandPart> errandPartStore)
        {
            _errandPartStore = errandPartStore;
        }
        public Task Combine(List<Errand> data)
        {
            List<ErrandPart> errandParts = GetErrandParts();

            foreach (Errand errand in data)
            {
                errand.Parts.Clear();
                errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandPart : ICombiner<Errand>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly IDataStore<Errand> _errandStore;

        public ErrandToErrandPart(ErrandPartStore errandPartStore, IDataStore<Errand> errandStore)
        {
            _errandPartStore = errandPartStore;
            _errandStore = errandStore;
        }
        public Task CombineAll()
        {
            List<ErrandPart> errandParts = GetErrandParts();
            List<Errand> errands = GetErrands();

            foreach (Errand item in errands)
            {
                Combine(item, errandParts);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(Errand item)
        {
            List<ErrandPart> errandParts = GetErrandParts();

            Combine(item, errandParts);

            return Task.CompletedTask;
        }
        private static void Combine(Errand errand, List<ErrandPart> errandParts)
        {
            errand.Parts.Clear();
            errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;
    }
}
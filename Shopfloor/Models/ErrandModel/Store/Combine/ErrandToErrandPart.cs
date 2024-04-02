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
        private readonly ErrandStore _errandStore;

        public ErrandToErrandPart(ErrandPartStore errandPartStore, ErrandStore errandStore)
        {
            _errandPartStore = errandPartStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<ErrandPart> errandParts = GetErrandParts();
            List<Errand> errands = GetErrands();

            foreach (Errand errand in errands)
            {
                errand.Parts.Clear();
                errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;
    }
}
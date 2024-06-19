using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrand : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandStore _errandStore;
        public ErrandPartToErrand(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;
        }
        public Task CombineAll()
        {
            List<Errand> errands = GetErrands();
            List<ErrandPart> errandParts = GetErrandParts();

            foreach (ErrandPart item in errandParts)
            {
                Combine(errands, item);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(ErrandPart item)
        {
            List<Errand> errands = GetErrands();

            Combine(errands, item);

            return Task.CompletedTask;
        }
        private static void Combine(List<Errand> errands, ErrandPart item)
        {
            item.Errand = errands.FirstOrDefault(errand => errand.Id == item.ErrandId);
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;

    }
}
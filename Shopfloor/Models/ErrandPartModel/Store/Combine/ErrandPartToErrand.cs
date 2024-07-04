using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrand : ICombiner<ErrandPart>
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly IDataStore<Errand> _errandStore;
        public ErrandPartToErrand(IDataStore<Errand> errandStore, IDataStore<ErrandPart> errandPartStore)
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
            item.Errand = errands.Find(errand => errand.Id == item.ErrandId);
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;
    }
}
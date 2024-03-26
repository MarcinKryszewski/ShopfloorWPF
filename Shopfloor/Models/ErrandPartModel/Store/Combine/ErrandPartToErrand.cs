using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrand : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly IDataStore<Errand> _errandStore;
        public ErrandPartToErrand(IDataStore<Errand> errandStore, ErrandPartStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;
        }
        public Task Combine()
        {
            List<Errand> errands = GetErrands();

            foreach (ErrandPart errandPart in _errandPartStore.Data)
            {
                errandPart.Errand = errands.FirstOrDefault(errand => errand.Id == errandPart.ErrandId);
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Errand> GetErrands() => _errandStore.Data;
    }
}
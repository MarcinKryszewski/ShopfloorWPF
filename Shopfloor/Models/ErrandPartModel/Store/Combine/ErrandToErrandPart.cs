using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandToErrandPart : ICombiner
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandStore _errandStore;
        public ErrandToErrandPart(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;
        }
        public Task Combine()
        {
            List<ErrandPart> errandParts = GetErrandParts();
            List<Errand> errands = GetErrands();

            foreach (ErrandPart errandPart in errandParts)
            {
                errandPart.Errand = errands.FirstOrDefault(errand => errand.Id == errandPart.ErrandId);
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
        private List<Errand> GetErrands() => _errandStore.GetData();
    }
}
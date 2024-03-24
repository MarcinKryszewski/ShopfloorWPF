using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandPartToErrand : ICombiner
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandStore _errandStore;
        public ErrandPartToErrand(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;
        }
        public Task Combine()
        {
            List<ErrandPart> errandParts = LoadErrandParts();
            List<Errand> errands = LoadErrands();

            foreach (Errand errand in errands)
            {
                errand.Parts.Clear();
                errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> LoadErrandParts() => _errandPartStore.GetData();
        private List<Errand> LoadErrands() => _errandStore.GetData();
    }
}
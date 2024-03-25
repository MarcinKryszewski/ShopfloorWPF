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
        public ErrandToErrandPart(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandPartStore = errandPartStore;
        }`
        public Task Combine(List<Errand> data)
        {
            List<ErrandPart> errandParts = GetErrandParts();
            //List<Errand> errands = GetErrands();

            foreach (Errand errand in data)
            {
                errand.Parts.Clear();
                errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
        //private List<Errand> GetErrands() => _errandStore.GetData();
    }
}
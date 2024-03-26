using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToErrand : ICombiner<ErrandPart>
    {
        private readonly IDataStore<Errand> _errandStore;
        public ErrandPartToErrand(IDataStore<Errand> errandStore)
        {
            _errandStore = errandStore;
        }
        public Task Combine(List<ErrandPart> data)
        {
            List<Errand> errands = GetErrands();

            foreach (ErrandPart errandPart in data)
            {
                errandPart.Errand = errands.FirstOrDefault(errand => errand.Id == errandPart.ErrandId);
            }
            return Task.CompletedTask;
        }
        private List<Errand> GetErrands() => _errandStore.GetData();
    }
}
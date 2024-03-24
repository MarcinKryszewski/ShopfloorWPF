using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandType : ICombiner
    {
        private readonly ErrandTypeStore _errandTypeStore;
        private readonly ErrandStore _errandStore;
        public ErrandToErrandType(ErrandStore errandStore, ErrandTypeStore errandPartStore)
        {
            _errandStore = errandStore;
            _errandTypeStore = errandPartStore;
        }
        public Task Combine()
        {
            List<ErrandType> types = LoadErrandTypes();
            List<Errand> errands = LoadErrands();

            foreach (Errand errand in errands)
            {
                errand.Type = types.FirstOrDefault(type => type.Id == errand.TypeId);
            }
            return Task.CompletedTask;
        }
        private List<ErrandType> LoadErrandTypes() => _errandTypeStore.GetData();
        private List<Errand> LoadErrands() => _errandStore.GetData();
    }
}
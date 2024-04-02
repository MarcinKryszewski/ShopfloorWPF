using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandType : ICombiner<Errand>
    {
        private readonly ErrandStore _errandStore;
        private readonly ErrandTypeStore _errandTypeStore;
        public ErrandToErrandType(ErrandTypeStore errandTypeStore, ErrandStore errandStore)
        {
            _errandTypeStore = errandTypeStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<Errand> errands = GetErrands();
            List<ErrandType> types = LoadErrandTypes();

            foreach (Errand errand in errands)
            {
                errand.Type = types.FirstOrDefault(type => type.Id == errand.TypeId);
            }
            return Task.CompletedTask;
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<ErrandType> LoadErrandTypes() => _errandTypeStore.Data;
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandType : ICombiner<Errand>
    {
        private readonly IDataStore<Errand> _errandStore;
        private readonly IDataStore<ErrandType> _errandTypeStore;
        public ErrandToErrandType(IDataStore<ErrandType> errandTypeStore, IDataStore<Errand> errandStore)
        {
            _errandTypeStore = errandTypeStore;
            _errandStore = errandStore;
        }
        public Task CombineAll()
        {
            List<Errand> errands = GetErrands();
            List<ErrandType> types = LoadErrandTypes();

            foreach (Errand item in errands)
            {
                Combine(item, types);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(Errand item)
        {
            List<ErrandType> types = LoadErrandTypes();

            Combine(item, types);

            return Task.CompletedTask;
        }
        private static void Combine(Errand errand, List<ErrandType> types)
        {
            errand.Type = types.FirstOrDefault(type => type.Id == errand.TypeId);
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<ErrandType> LoadErrandTypes() => _errandTypeStore.Data;
    }
}
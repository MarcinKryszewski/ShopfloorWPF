using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToErrandType : ICombiner<Errand>
    {
        private readonly IDataStore<ErrandType> _errandTypeStore;
        public ErrandToErrandType(IDataStore<ErrandType> errandTypeStore)
        {
            _errandTypeStore = errandTypeStore;
        }
        public Task Combine(List<Errand> data)
        {
            List<ErrandType> types = LoadErrandTypes();

            foreach (Errand errand in data)
            {
                errand.Type = types.FirstOrDefault(type => type.Id == errand.TypeId);
            }
            return Task.CompletedTask;
        }
        private List<ErrandType> LoadErrandTypes() => _errandTypeStore.GetData();
    }
}
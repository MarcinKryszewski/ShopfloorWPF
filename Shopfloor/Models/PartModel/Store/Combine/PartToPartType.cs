using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartToPartType : ICombiner<Part>
    {
        private readonly PartTypeStore _typesStore;
        public PartToPartType(PartTypeStore typesStore)
        {
            _typesStore = typesStore;
        }
        public Task Combine(List<Part> data)
        {
            List<PartType> types = GetTypes();

            foreach (Part part in data)
            {
                part.SetType(types.FirstOrDefault(type => type.Id == part.TypeId));
            }
            return Task.CompletedTask;
        }
        private List<PartType> GetTypes() => _typesStore.GetData();
    }
}
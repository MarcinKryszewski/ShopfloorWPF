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
        private readonly PartStore _partsStore;
        public PartToPartType(PartTypeStore typesStore, PartStore partsStore)
        {
            _typesStore = typesStore;
            _partsStore = partsStore;
        }
        public Task Combine()
        {
            List<PartType> types = GetTypes();
            List<Part> parts = GetParts();

            foreach (Part part in parts)
            {
                part.SetType(types.FirstOrDefault(type => type.Id == part.TypeId));
            }
            return Task.CompletedTask;
        }
        private List<PartType> GetTypes() => _typesStore.Data;
        private List<Part> GetParts() => _partsStore.Data;
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartToPartType : ICombiner<Part>
    {
        private readonly IDataStore<Part> _partsStore;
        private readonly IDataStore<PartType> _typesStore;
        public PartToPartType(IDataStore<PartType> typesStore, IDataStore<Part> partsStore)
        {
            _typesStore = typesStore;
            _partsStore = partsStore;
        }
        public Task CombineAll()
        {
            List<PartType> types = GetTypes();
            List<Part> parts = GetParts();

            foreach (Part item in parts)
            {
                Combine(types, item);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(Part item)
        {
            List<PartType> types = GetTypes();
            Combine(types, item);
            return Task.CompletedTask;
        }
        private static void Combine(List<PartType> types, Part item)
        {
            item.PartType = types.Find(type => type.Id == item.TypeId);
        }
        private List<Part> GetParts() => _partsStore.Data;
        private List<PartType> GetTypes() => _typesStore.Data;
    }
}
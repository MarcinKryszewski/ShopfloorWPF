using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using System;
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
        private static void Combine(List<PartType> types, Part item)
        {
            item.PartType = types.FirstOrDefault(type => type.Id == item.TypeId);
        }
        private List<PartType> GetTypes() => _typesStore.Data;
        private List<Part> GetParts() => _partsStore.Data;
        public Task CombineOne(Part item)
        {
            List<PartType> types = GetTypes();
            Combine(types, item);
            return Task.CompletedTask;
        }
    }
}
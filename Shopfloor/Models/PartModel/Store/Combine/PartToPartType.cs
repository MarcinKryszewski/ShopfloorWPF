using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;

namespace Shopfloor.Models.PartModel.Store.Combine
{
    internal sealed class PartToPartType : ICombiner
    {
        private readonly PartStore _partStore;
        private readonly PartTypeStore _typesStore;
        public PartToPartType(PartTypeStore typesStore, PartStore partStore)
        {
            _typesStore = typesStore;
            _partStore = partStore;
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
        private List<Part> GetParts() => _partStore.GetData();
        private List<PartType> GetTypes() => _typesStore.GetData();
    }
}
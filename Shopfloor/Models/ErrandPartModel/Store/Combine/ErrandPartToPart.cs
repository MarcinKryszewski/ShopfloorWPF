using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToPart : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly PartStore _partStore;
        public ErrandPartToPart(PartStore partStore, ErrandPartStore errandPartStore)
        {
            _partStore = partStore;
            _errandPartStore = errandPartStore;
        }
        public Task CombineAll()
        {
            List<Part> parts = GetParts();

            foreach (ErrandPart item in GetErrandParts())
            {
                Combine(parts, item);
            }
            return Task.CompletedTask;
        }

        private static void Combine(List<Part> parts, ErrandPart item)
        {
            item.Part = parts.FirstOrDefault(part => part.Id == item.PartId);
        }

        public Task CombineOne(ErrandPart item)
        {
            List<Part> parts = GetParts();

            Combine(parts, item);

            return Task.CompletedTask;
        }

        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Part> GetParts() => _partStore.Data;
    }
}
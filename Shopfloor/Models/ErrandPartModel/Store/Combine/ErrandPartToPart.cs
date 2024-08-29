using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToPart : ICombiner<ErrandPart>
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly IDataStore<Part> _partStore;
        private readonly ICombinerManager<Part> _partCombiner;
        public ErrandPartToPart(IDataStore<Part> partStore, IDataStore<ErrandPart> errandPartStore, ICombinerManager<Part> partCombiner)
        {
            _partStore = partStore;
            _errandPartStore = errandPartStore;
            _partCombiner = partCombiner;
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

        public Task CombineOne(ErrandPart item)
        {
            List<Part> parts = GetParts();

            Combine(parts, item);

            return Task.CompletedTask;
        }
        private static void Combine(List<Part> parts, ErrandPart item)
        {
            item.Part = parts.Find(part => part.Id == item.PartId);
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Part> GetParts()
        {
            _partCombiner.CombineAll().Wait();
            return _partStore.Data;
        }
    }
}
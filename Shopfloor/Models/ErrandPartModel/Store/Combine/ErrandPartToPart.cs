using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToPart : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly IDataStore<Part> _partStore;
        public ErrandPartToPart(IDataStore<Part> partStore, ErrandPartStore errandPartStore)
        {
            _partStore = partStore;
            _errandPartStore = errandPartStore;
        }
        public Task Combine()
        {
            List<Part> parts = GetParts();

            foreach (ErrandPart errandPart in GetErrandParts())
            {
                errandPart.Part = parts.FirstOrDefault(part => part.Id == errandPart.PartId);
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<Part> GetParts() => _partStore.Data;
    }
}
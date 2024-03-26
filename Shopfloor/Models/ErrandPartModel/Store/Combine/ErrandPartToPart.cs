using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToPart : ICombiner<ErrandPart>
    {
        private readonly IDataStore<Part> _partStore;
        public ErrandPartToPart(IDataStore<Part> partStore)
        {
            _partStore = partStore;
        }
        public Task Combine(List<ErrandPart> data)
        {
            List<Part> parts = GetParts();

            foreach (ErrandPart errandPart in data)
            {
                errandPart.Part = parts.FirstOrDefault(part => part.Id == errandPart.PartId);
            }
            return Task.CompletedTask;
        }
        private List<Part> GetParts() => _partStore.GetData();
    }
}
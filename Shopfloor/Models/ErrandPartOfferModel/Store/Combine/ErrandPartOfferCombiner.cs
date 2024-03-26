using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartOfferModel.Store
{
    internal sealed class ErrandPartOfferCombiner : ICombiner<ErrandPartOffer>
    {
        public ErrandPartOfferCombiner()
        {
        }
        public Task Combine(List<ErrandPartOffer> data)
        {
            return Task.CompletedTask;
        }
    }
}
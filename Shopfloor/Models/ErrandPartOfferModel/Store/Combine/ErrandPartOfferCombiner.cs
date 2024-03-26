using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartOfferModel.Store
{
    internal sealed class ErrandPartOfferCombiner : ICombinerManager<ErrandPartOffer>
    {
        public ErrandPartOfferCombiner()
        {
        }
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}
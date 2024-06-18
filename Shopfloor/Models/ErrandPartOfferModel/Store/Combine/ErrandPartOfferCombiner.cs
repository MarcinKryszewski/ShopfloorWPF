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
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }
    }
}
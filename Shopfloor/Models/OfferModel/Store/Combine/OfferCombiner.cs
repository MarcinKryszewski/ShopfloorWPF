using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OfferModel.Store.Combine
{
    internal sealed class OfferCombiner : ICombinerManager<Offer>
    {
        public bool IsCombined { get; private set; }
        public Task Combine(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }
    }
}
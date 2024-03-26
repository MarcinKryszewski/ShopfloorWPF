using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OfferModel.Store.Combine
{
    internal sealed class OfferCombiner : ICombiner<Offer>
    {
        public Task Combine(List<Offer> data)
        {
            return Task.CompletedTask;
        }
    }
}
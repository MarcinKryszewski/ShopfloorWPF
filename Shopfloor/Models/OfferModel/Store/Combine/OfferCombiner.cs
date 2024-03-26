using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OfferModel.Store.Combine
{
    internal sealed class OfferCombiner : ICombinerManager<Offer>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}
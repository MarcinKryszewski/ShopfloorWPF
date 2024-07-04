using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OfferModel.Store.Combine
{
    internal sealed class OfferCombiner : ICombinerManager<Offer>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return Task.CompletedTask;
            }

            IsCombined = true;
            return Task.CompletedTask;
        }

        public Task CombineOne(Offer item)
        {
            return Task.CompletedTask;
        }
    }
}
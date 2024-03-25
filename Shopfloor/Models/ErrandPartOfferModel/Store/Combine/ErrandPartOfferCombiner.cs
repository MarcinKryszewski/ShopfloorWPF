using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartOfferModel.Store
{
    internal sealed class ErrandPartOfferCombiner : ICombiner
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
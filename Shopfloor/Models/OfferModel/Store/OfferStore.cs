using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.OfferModel.Store.Combine;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferStore : StoreBase<Offer>
    {
        public OfferStore(OfferProvider provider, OfferCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
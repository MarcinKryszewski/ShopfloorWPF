using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandStatusModel.Store.Combine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusStore : StoreBase<ErrandStatus>
    {
        public ErrandStatusStore(ErrandStatusProvider provider, ErrandStatusCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
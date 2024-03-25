using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel.Store.Combine;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartStore : StoreBase<Part>
    {
        public PartStore(PartProvider provider, PartCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
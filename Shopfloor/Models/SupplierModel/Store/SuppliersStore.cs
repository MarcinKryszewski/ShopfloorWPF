using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.SupplierModel.Store.Combine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SuppliersStore : StoreBase<Supplier>
    {
        public SuppliersStore(SupplierProvider provider, SupplierCombiner combiner) : base(provider, combiner)
        {

        }
    }
}
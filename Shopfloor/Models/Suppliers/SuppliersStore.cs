using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SuppliersStore : IDataStore<Supplier>
    {
        private IEnumerable<Supplier> _data = Enumerable.Empty<Supplier>();
        private readonly IServiceProvider _databaseServices;
        public IEnumerable<Supplier> Data => _data;
        public bool IsLoaded { get; private set; }

        public SuppliersStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            IProvider<Supplier> provider = _databaseServices.GetRequiredService<SupplierProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
    }
}
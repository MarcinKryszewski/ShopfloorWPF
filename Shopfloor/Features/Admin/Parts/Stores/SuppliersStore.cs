using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class SuppliersStore : IDataStore<Supplier>
    {
        private IEnumerable<Supplier> _suppliers = Enumerable.Empty<Supplier>();
        public IEnumerable<Supplier> Data
        {
            get => _suppliers;
            set
            {
                if (value is null) return;
                _suppliers = value;
                IsLoaded = true;
            }
        }
        public bool IsLoaded { get; private set; }
    }
}
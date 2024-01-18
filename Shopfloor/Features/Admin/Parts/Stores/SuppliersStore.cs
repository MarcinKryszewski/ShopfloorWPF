using System.Collections.Generic;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class SuppliersStore : IDataStore<Supplier>
    {
        private IEnumerable<Supplier>? _suppliers;
        public IEnumerable<Supplier>? Data
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                if (value is null)
                {
                    IsLoaded = false;
                }
                IsLoaded = true;
            }
        }
        public bool IsLoaded { get; private set; }
    }
}
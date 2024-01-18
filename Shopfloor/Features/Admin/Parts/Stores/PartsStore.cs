using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class PartsStore : IDataStore<Part>
    {
        private IEnumerable<Part> _parts = Enumerable.Empty<Part>();
        public IEnumerable<Part> Data
        {
            get => _parts;
            set
            {
                if (value is null) return;
                _parts = value;
                IsLoaded = true;
            }
        }
        public bool IsLoaded { get; private set; }
    }
}
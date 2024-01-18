using System.Collections.Generic;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class PartsStore : IDataStore<Part>
    {
        private IEnumerable<Part>? _parts;
        public IEnumerable<Part>? Data
        {
            get => _parts;
            set
            {
                _parts = value;
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
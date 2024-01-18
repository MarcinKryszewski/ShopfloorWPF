using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class PartTypesStore : IDataStore<PartType>
    {
        private IEnumerable<PartType> _partTypes = Enumerable.Empty<PartType>();
        public IEnumerable<PartType> Data
        {
            get => _partTypes;
            set
            {
                if (value is null) return;
                _partTypes = value;
                IsLoaded = true;
            }
        }
        public bool IsLoaded { get; private set; }
    }
}
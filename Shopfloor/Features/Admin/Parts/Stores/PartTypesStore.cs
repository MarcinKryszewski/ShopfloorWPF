using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Parts.Stores
{
    public class PartTypesStore : IDataStore<PartType>
    {
        private IEnumerable<PartType>? _partTypes;
        public IEnumerable<PartType>? Data
        {
            get => _partTypes;
            set
            {
                _partTypes = value;
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
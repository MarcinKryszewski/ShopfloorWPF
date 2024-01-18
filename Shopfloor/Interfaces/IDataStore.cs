using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    public interface IDataStore<T>
    {
        public IEnumerable<T>? Data { get; set; }
        public bool IsLoaded { get; }
    }
}
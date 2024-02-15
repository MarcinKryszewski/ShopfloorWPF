using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface IDataStore<T>
    {
        public List<T> Data { get; }
        public bool IsLoaded { get; }

        public Task Load();
    }
}
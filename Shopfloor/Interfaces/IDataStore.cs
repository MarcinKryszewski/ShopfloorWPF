using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    public interface IDataStore<T>
    {
        public IEnumerable<T> Data { get; }
        public bool IsLoaded { get; }
        public Task Load();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface IDataStore<T>
    {
        public List<T> GetData();
        public bool IsLoaded { get; }
        public Task Reload();
    }
}
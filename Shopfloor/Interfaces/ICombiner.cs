using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface ICombiner<T>
    {
        public Task Combine(List<T> data);
    }
    internal interface ICombinerManager<T>
    {
        public Task Combine();
    }
}
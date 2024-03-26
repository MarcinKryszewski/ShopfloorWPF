using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface ICombiner<T>
    {
        public Task Combine();
    }
    internal interface ICombinerManager<T>
    {
        public Task Combine();
    }
}
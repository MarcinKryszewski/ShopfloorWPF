using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface ICombiner<T>
    {
        public Task CombineAll();
        public Task CombineOne(T item);
    }
    internal interface ICombinerManager<T>
    {
        public Task CombineAll(bool shouldForce);
        public Task CombineOne(T item);
        public bool IsCombined { get; }
    }
}
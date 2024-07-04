using System.Threading.Tasks;

namespace Shopfloor.Interfaces
{
    internal interface ICombiner<in T>
    {
        public Task CombineAll();
        public Task CombineOne(T item);
    }
    internal interface ICombinerManager<in T>
    {
        public bool IsCombined { get; }
        public Task CombineAll(bool shouldForce = false);
        public Task CombineOne(T item);
    }
}
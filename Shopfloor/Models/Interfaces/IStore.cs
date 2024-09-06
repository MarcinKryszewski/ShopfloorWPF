using System.Collections.Generic;

namespace Shopfloor.Models.Interfaces
{
    public interface IStore<T>
        where T : IModel
    {
        public IReadOnlyList<T> Data();
    }
}
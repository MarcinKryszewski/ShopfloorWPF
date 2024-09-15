using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.BaseClasses;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IRepository<T, TCreate>
        where T : IModel
        where TCreate : ModelValidationBase
    {
        public Task<T> Create(TCreate item);
        public Task<T> Delete();
        public Task<List<T>> GetData();
        public Task<T> Update();
    }
}
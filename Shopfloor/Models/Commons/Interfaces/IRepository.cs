using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.BaseClasses;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IRepository<T, TCreate>
        where T : IModel
        where TCreate : ModelValidationBase
    {
        public HashSet<Type> Merges { get; }
        public Task<T> Create(TCreate item);
        public Task Delete(int id);
        public Task<List<T>> GetDataAsync();
        public Task Update(TCreate item);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Parts
{
    internal class PartProvider : IProvider<PartModel>
    {
        public Task<int> Create(PartModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<PartModel>> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<PartModel> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task Update(PartModel item)
        {
            throw new NotImplementedException();
        }
    }
}
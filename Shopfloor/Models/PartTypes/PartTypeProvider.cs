using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Parts;

namespace Shopfloor.Models.PartTypes
{
    internal class PartTypeProvider : IProvider<PartTypeModel, PartTypeCreationModel>
    {
        public Task<int> Create(PartTypeCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<PartTypeModel>> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<PartTypeModel> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task Update(PartTypeModel item)
        {
            throw new NotImplementedException();
        }
    }
}
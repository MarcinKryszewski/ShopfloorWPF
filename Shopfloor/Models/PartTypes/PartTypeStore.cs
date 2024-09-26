using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PartTypes
{
    internal class PartTypeStore : IStore<PartTypeModel>
    {
        private readonly List<PartTypeModel> _data = [];
        public List<PartTypeModel> Data => _data;
        public Task AddItem(PartTypeModel item)
        {
            throw new NotImplementedException();
        }
        public Task ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}
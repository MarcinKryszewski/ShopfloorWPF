using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Manufacturers
{
    internal class ManufacturerStore : IStore<ManufacturerModel>
    {
        private readonly List<ManufacturerModel> _data = [];
        public List<ManufacturerModel> Data => _data;
        public Task AddItem(ManufacturerModel item)
        {
            throw new NotImplementedException();
        }

        public Task ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}
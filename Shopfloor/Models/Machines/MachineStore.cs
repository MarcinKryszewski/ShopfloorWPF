using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    internal class MachineStore : IStore<MachineModel>
    {
        private readonly List<MachineModel> _data = [];
        public List<MachineModel> Data => _data;
        public Task AddItem(MachineModel item)
        {
            throw new NotImplementedException();
        }

        public Task ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}
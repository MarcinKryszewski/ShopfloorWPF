using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.MachinesResponsibles
{
    internal class MachineResponsibleStore : IStore<MachineResponsible>
    {
        private readonly List<MachineResponsible> _data = [];
        public List<MachineResponsible> Data => _data;
        public Task AddItem(MachineResponsible item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public Task ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}
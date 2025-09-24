using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SubstanceHazards
{
    internal class SubstanceHazardStore : IStore<SubstanceHazard>
    {
        private readonly List<SubstanceHazard> _data = [];
        public List<SubstanceHazard> Data => _data;
        public Task AddItem(SubstanceHazard item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
            await Task.Delay(0);
        }
    }
}
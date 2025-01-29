using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.MachinesResponsibles
{
    internal class MachineResponsibleRepository : IRepository<MachineResponsible, MachineResponsibleCreation>
    {
        private readonly IProvider<MachineResponsible, MachineResponsibleCreation> _provider;
        private readonly IStore<MachineResponsible> _store;
        private bool _dataLoaded = false;
        private bool _loading = false;
        public MachineResponsibleRepository(IStore<MachineResponsible> store, IProvider<MachineResponsible, MachineResponsibleCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<MachineResponsible> Create(MachineResponsibleCreation item)
        {
            int id = await _provider.Create(item);
            MachineResponsible model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            string errorText = "ERROR";
            MachineResponsible? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _provider.Delete(id);
                _store.Data.Remove(item);
            }
            catch (Exception)
            {
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<List<MachineResponsible>> GetDataAsync()
        {
            while (_loading)
            {
                await Task.Delay(3);
            }
            if (!_dataLoaded)
            {
                _loading = true;
                _dataLoaded = true;
                List<MachineResponsible> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _loading = false;
            }

            return _store.Data;
        }
        public Task Update(MachineResponsibleCreation item)
        {
            throw new NotSupportedException();
        }
    }
}
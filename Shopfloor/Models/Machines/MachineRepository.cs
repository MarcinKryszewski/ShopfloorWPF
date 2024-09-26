using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    internal class MachineRepository : IRepository<MachineModel, MachineCreationModel>
    {
        private readonly IStore<MachineModel> _store;
        private bool _dataLoaded = false;
        public MachineRepository(IStore<MachineModel> store)
        {
            _store = store;
        }
        public HashSet<Type> Merges { get; } = [];
        public Task<MachineModel> Create(MachineCreationModel item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<MachineModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<MachineModel> data = [
                    new MachineModel { Id = 1, Name = "CNC Lathe", LineId = 1 },
                    new MachineModel { Id = 2, Name = "Milling Machine", LineId = 2 },
                    new MachineModel { Id = 3, Name = "3D Printer", LineId = 3 },
                    new MachineModel { Id = 4, Name = "Laser Cutter", LineId = 1 },
                    new MachineModel { Id = 5, Name = "Hydraulic Press", LineId = 4 },
                    new MachineModel { Id = 6, Name = "Injection Molding Machine", LineId = 2 },
                    new MachineModel { Id = 7, Name = "Drilling Machine", LineId = 5 },
                    new MachineModel { Id = 8, Name = "Grinder", LineId = 3 },
                    new MachineModel { Id = 9, Name = "Robotic Arm", LineId = 4 },
                    new MachineModel { Id = 10, Name = "Welder", LineId = 5 },
                ];

                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }
        public Task Update(MachineCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}
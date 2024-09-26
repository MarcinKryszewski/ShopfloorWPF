using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Manufacturers;
using Shopfloor.Models.Parts;
using Shopfloor.Models.PartTypes;

namespace Shopfloor.Roots
{
    internal class PartsListRoot : IRoot
    {
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<MachineModel, MachineCreationModel> _machineRepository;
        private readonly IRepository<ManufacturerModel, ManufacturerCreationModel> _manufacturersRepository;
        private readonly IRepository<PartTypeModel, PartTypeCreationModel> _partTypesRepository;
        private readonly IRepository<PartModel, PartCreationModel> _partRepository;
        public PartsListRoot(
            IRepository<PartModel, PartCreationModel> partRepository,
            IRepository<LineModel, LineCreationModel> lineRepository,
            IRepository<MachineModel, MachineCreationModel> machineRepository,
            IRepository<ManufacturerModel, ManufacturerCreationModel> manufacturersRepository,
            IRepository<PartTypeModel, PartTypeCreationModel> partTypesRepository)
        {
            _partRepository = partRepository;
            _lineRepository = lineRepository;
            _machineRepository = machineRepository;
            _manufacturersRepository = manufacturersRepository;
            _partTypesRepository = partTypesRepository;
        }
        public event EventHandler? DataChanged;
        public async Task<IEnumerable<PartModel>> GetParts()
        {
            try
            {
                if (!_partRepository.Merges.Contains(typeof(ManufacturerModel)))
                {
                    _ = DecorateWithManufacturers();
                }
                if (!_partRepository.Merges.Contains(typeof(PartTypeModel)))
                {
                    _ = DecorateWithTypes();
                }
                return await _partRepository.GetDataAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<LineModel>> GetLines()
        {
            try
            {
                return await _lineRepository.GetDataAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<string>> GetIndexes()
        {
            try
            {
                IEnumerable<PartModel> parts = await GetParts();
                return parts.Select(part => part.Index).Distinct();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<MachineModel>> GetMachines()
        {
            try
            {
                return await _machineRepository.GetDataAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<string>> GetManufacturerNumbers()
        {
            try
            {
                IEnumerable<PartModel> parts = await GetParts();
                return parts.Select(part => part.PartNumber).Distinct();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<ManufacturerModel>> GetManufacturers()
        {
            try
            {
                return await _manufacturersRepository.GetDataAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<IEnumerable<PartTypeModel>> GetTypes()
        {
            try
            {
                return await _partTypesRepository.GetDataAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateWithManufacturers()
        {
            Task<List<PartModel>> partsTask = _partRepository.GetDataAsync();
            Task<List<ManufacturerModel>> manufacturerTask = _manufacturersRepository.GetDataAsync();

            await Task.WhenAll(partsTask, manufacturerTask);

            List<PartModel> parts = partsTask.Result;
            List<ManufacturerModel> manufacturers = manufacturerTask.Result;

            Dictionary<int, ManufacturerModel>? manufacturerDictionary = manufacturers.ToDictionary(manufacturer => manufacturer.Id);
            foreach (PartModel part in parts)
            {
                part.Manufacturer = manufacturerDictionary.TryGetValue(part.ManufacturerId, out ManufacturerModel? manufacturer) ? manufacturer : null;
            }
            _partRepository.Merges.Add(typeof(ManufacturerModel));
            OnDataChanged(EventArgs.Empty);
        }
        private async Task DecorateWithTypes()
        {
            Task<List<PartModel>> partsTask = _partRepository.GetDataAsync();
            Task<List<PartTypeModel>> typeTask = _partTypesRepository.GetDataAsync();

            await Task.WhenAll(partsTask, typeTask);

            List<PartModel> parts = partsTask.Result;
            List<PartTypeModel> types = typeTask.Result;

            Dictionary<int, PartTypeModel>? typeDictionary = types.ToDictionary(line => line.Id);
            foreach (PartModel part in parts)
            {
                part.PartType = typeDictionary.TryGetValue(part.PartTypeId, out PartTypeModel? type) ? type : null;
            }
            _partRepository.Merges.Add(typeof(PartTypeModel));
            OnDataChanged(EventArgs.Empty);
        }
    }
}
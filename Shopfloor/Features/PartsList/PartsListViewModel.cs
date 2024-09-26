using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Manufacturers;
using Shopfloor.Models.Parts;
using Shopfloor.Models.PartTypes;
using Shopfloor.Roots;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.PartsList
{
    internal class PartsListViewModel : ViewModelBase
    {
        private readonly List<PartModel> _parts = [];
        private readonly List<LineModel> _lines = [];
        private readonly List<MachineModel> _machines = [];
        private readonly List<PartTypeModel> _types = [];
        private readonly List<ManufacturerModel> _manufacturers = [];
        private readonly List<string> _manufacturerNumbers = [];
        private readonly List<string> _indexes = [];
        private readonly PartsListRoot _root;
        public PartsListViewModel(PartsListRoot root)
        {
            _root = root;
            _root.DataChanged += DataChanged;
            PartsFilter.FiltersChanged += FiltersChanged;

            Parts.Filter = FilterParts;

            _ = LoadDataAsync();
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICollectionView Types => CollectionViewSource.GetDefaultView(_types);
        public ICollectionView Manufacturers => CollectionViewSource.GetDefaultView(_manufacturers);
        public ICollectionView ManufacturerNumbers => CollectionViewSource.GetDefaultView(_manufacturerNumbers);
        public ICollectionView Indexes => CollectionViewSource.GetDefaultView(_indexes);
        public PartsFilter PartsFilter { get; set; } = new();
        public void DataChanged(object? sender, EventArgs e)
        {
            Parts.Refresh();
        }
        public void FiltersChanged(object? sender, EventArgs e)
        {
            Parts.Refresh();
        }
        private bool FilterParts(object obj)
        {
            if (obj is PartModel part)
            {
                bool index = string.IsNullOrEmpty(PartsFilter.Index) || part.Index.Contains(PartsFilter.Index, StringComparison.InvariantCultureIgnoreCase);
                bool type = string.IsNullOrEmpty(PartsFilter.Type) || (part.PartType?.Name.Contains(PartsFilter.Type, StringComparison.InvariantCultureIgnoreCase) ?? true);
                // bool machine = string.IsNullOrEmpty(PartsFilter.Type) || (part.PartType?.Name.Contains(PartsFilter.Type, StringComparison.InvariantCultureIgnoreCase) ?? true);
                bool manufacturer = string.IsNullOrEmpty(PartsFilter.Manufacturer) || (part.Manufacturer?.Name.Contains(PartsFilter.Manufacturer, StringComparison.InvariantCultureIgnoreCase) ?? true);
                bool confirmed = !PartsFilter.Confirmed || part.IsConfirmed;
                return index && type && manufacturer && confirmed;
            }
            return false;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<PartModel> parts = await _root.GetParts();
            IEnumerable<LineModel> lines = await _root.GetLines();
            IEnumerable<MachineModel> machines = await _root.GetMachines();
            IEnumerable<PartTypeModel> types = await _root.GetTypes();
            IEnumerable<ManufacturerModel> manufacturers = await _root.GetManufacturers();
            IEnumerable<string> indexes = await _root.GetIndexes();
            IEnumerable<string> manufacturerNumbers = await _root.GetManufacturerNumbers();

            tasks.Add(BatchListUpdater.UpdateAsync(
                parts,
                _parts,
                Parts));

            tasks.Add(BatchListUpdater.UpdateAsync(
                lines,
                _lines,
                Lines));

            tasks.Add(BatchListUpdater.UpdateAsync(
                machines,
                _machines,
                Machines));

            tasks.Add(BatchListUpdater.UpdateAsync(
                types,
                _types,
                Types));

            tasks.Add(BatchListUpdater.UpdateAsync(
                manufacturers,
                _manufacturers,
                Manufacturers));

            tasks.Add(BatchListUpdater.UpdateAsync(
                indexes,
                _indexes,
                Indexes));

            tasks.Add(BatchListUpdater.UpdateAsync(
                manufacturerNumbers,
                _manufacturerNumbers,
                ManufacturerNumbers));

            await Task.WhenAll(tasks);
        }
    }
}
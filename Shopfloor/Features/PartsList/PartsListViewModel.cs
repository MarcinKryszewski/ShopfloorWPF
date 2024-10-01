using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.PartsList.Commands;
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
        private readonly PartsBasketContext _basket;
        public PartsListViewModel(PartsListRoot root, PartsBasketContext basket, ViewModelBaseDependecies baseDependecies)
        : base(baseDependecies)
        {
            _root = root;
            _basket = basket;
            _root.DataChanged += DataChanged;
            PartsFilter.FiltersChanged += FiltersChanged;

            Parts.Filter = FilterParts;
            _ = LoadDataAsync();

            AddPartCommand = new AddToBasketCommand(basket, baseDependecies.Notifier);
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICollectionView Types => CollectionViewSource.GetDefaultView(_types);
        public ICollectionView Manufacturers => CollectionViewSource.GetDefaultView(_manufacturers);
        public ICollectionView ManufacturerNumbers => CollectionViewSource.GetDefaultView(_manufacturerNumbers);
        public ICollectionView Indexes => CollectionViewSource.GetDefaultView(_indexes);
        public ICommand AddPartCommand { get; }
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
                bool manufacturer = string.IsNullOrEmpty(PartsFilter.Manufacturer) || (part.Manufacturer?.Name.Contains(PartsFilter.Manufacturer, StringComparison.InvariantCultureIgnoreCase) ?? true);
                bool confirmed = !PartsFilter.Confirmed || part.IsConfirmed;

                return index && type && manufacturer && confirmed;
            }
            return false;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadPartsAsync());
            tasks.Add(LoadLinesAsync());
            tasks.Add(LoadMachinesAsync());
            tasks.Add(LoadTypesAsync());
            tasks.Add(LoadManufacturersAsync());
            tasks.Add(LoadIndexesAsync());
            tasks.Add(LoadManufacturerNumbersAsync());

            await Task.WhenAll(tasks);
        }
        private async Task LoadPartsAsync()
        {
            IEnumerable<PartModel> parts = await _root.GetParts();
            await BatchListUpdater.UpdateAsync(parts, _parts, Parts);
        }
        private async Task LoadLinesAsync()
        {
            IEnumerable<LineModel> lines = await _root.GetLines();
            await BatchListUpdater.UpdateAsync(lines, _lines, Lines);
        }
        private async Task LoadMachinesAsync()
        {
            IEnumerable<MachineModel> machines = await _root.GetMachines();
            await BatchListUpdater.UpdateAsync(machines, _machines, Machines);
        }
        private async Task LoadTypesAsync()
        {
            IEnumerable<PartTypeModel> types = await _root.GetTypes();
            await BatchListUpdater.UpdateAsync(types, _types, Types);
        }
        private async Task LoadManufacturersAsync()
        {
            IEnumerable<ManufacturerModel> manufacturers = await _root.GetManufacturers();
            await BatchListUpdater.UpdateAsync(manufacturers, _manufacturers, Manufacturers);
        }
        private async Task LoadIndexesAsync()
        {
            IEnumerable<string> indexes = await _root.GetIndexes();
            await BatchListUpdater.UpdateAsync(indexes, _indexes, Indexes);
        }
        private async Task LoadManufacturerNumbersAsync()
        {
            IEnumerable<string> manufacturerNumbers = await _root.GetManufacturerNumbers();
            await BatchListUpdater.UpdateAsync(manufacturerNumbers, _manufacturerNumbers, ManufacturerNumbers);
        }
    }
}
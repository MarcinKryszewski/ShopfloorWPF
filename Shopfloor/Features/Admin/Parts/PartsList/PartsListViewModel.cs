using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Add;
using Shopfloor.Features.Admin.Parts.Edit;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores.DatabaseDataStores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts.List
{
    public class PartsListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _databaseServices;
        private readonly IServiceProvider _mainServices;
        private string _searchText = string.Empty;
        private readonly ObservableCollection<Part> _parts;
        private readonly SelectedPartStore _selectedPart;

        private readonly PartsStore _partsStore;
        private readonly SuppliersStore _suppliersStore;
        private readonly PartTypesStore _partTypesStore;

        public Visibility IsSelected => SelectedPart is null ? Visibility.Collapsed : Visibility.Visible;
        public ICollectionView Parts { get; }
        public Part? SelectedPart
        {
            get => _selectedPart.Part;
            set
            {
                if (_selectedPart.Part == value) return;

                _selectedPart.Part = value;

                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(SelectedPart));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Parts.Filter = FilterParts;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand AddPartCommand { get; }
        public ICommand EditPartCommand { get; }

        public PartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _mainServices = mainServices;
            _parts = new();
            _selectedPart = _mainServices.GetRequiredService<SelectedPartStore>();

            AddPartCommand = new NavigateCommand<PartsAddViewModel>(mainServices.GetRequiredService<NavigationService<PartsAddViewModel>>());
            EditPartCommand = new NavigateCommand<PartsEditViewModel>(mainServices.GetRequiredService<NavigationService<PartsEditViewModel>>());

            Parts = CollectionViewSource.GetDefaultView(_parts);

            _partsStore = _databaseServices.GetRequiredService<PartsStore>();
            _suppliersStore = _databaseServices.GetRequiredService<SuppliersStore>();
            _partTypesStore = _databaseServices.GetRequiredService<PartTypesStore>();

            Task.Run(LoadData);
            Parts.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Part.TypeName)));
        }

        public async Task LoadData()
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();

            List<Task> tasks = new();

            if (!_partsStore.IsLoaded) tasks.Add(LoadParts());
            if (!_suppliersStore.IsLoaded) tasks.Add(LoadSuppliers());
            if (!_partTypesStore.IsLoaded) tasks.Add(LoadPartTypes());

            if (tasks.Count > 0) await Task.WhenAll(tasks);

            IEnumerable<Part> parts = _partsStore.Data;
            IEnumerable<Supplier> suppliers = _suppliersStore.Data;
            IEnumerable<PartType> partTypes = _partTypesStore.Data;

            foreach (Part part in parts)
            {
                PartType? partType = partTypes.FirstOrDefault(pt => pt.Id == part.TypeId);
                if (partType is not null) part.SetType(partType);

                Supplier? producer = suppliers.FirstOrDefault(p => p.Id == part.ProducerId);
                if (producer is not null) part.SetProducer(producer);

                Supplier? supplier = suppliers.FirstOrDefault(s => s.Id == part.SupplierId);
                if (supplier is not null) part.SetSupplier(supplier);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _parts.Add(part);
                    OnPropertyChanged(nameof(Parts));
                });
            }

            //stopwatch.Stop();
            //Debug.WriteLine(stopwatch.ElapsedTicks);
        }

        public Task LoadParts()
        {
            _partsStore.Load();
            return Task.CompletedTask;
        }
        public Task LoadPartTypes()
        {
            _partTypesStore.Load();
            return Task.CompletedTask;
        }
        public Task LoadSuppliers()
        {
            _suppliersStore.Load();
            return Task.CompletedTask;
        }

        private bool FilterParts(object obj)
        {
            if (obj is Part part)
            {
                return part.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
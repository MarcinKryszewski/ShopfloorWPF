using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Add;
using Shopfloor.Features.Admin.Parts.Edit;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
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
        private string _searchText = string.Empty;
        private readonly ObservableCollection<Part> _parts;
        private SelectedPartStore _selectedPart;

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
            _parts = new();
            _selectedPart = mainServices.GetRequiredService<SelectedPartStore>();

            AddPartCommand = new NavigateCommand<PartsAddViewModel>(mainServices.GetRequiredService<NavigationService<PartsAddViewModel>>());
            EditPartCommand = new NavigateCommand<PartsEditViewModel>(mainServices.GetRequiredService<NavigationService<PartsEditViewModel>>());

            Parts = CollectionViewSource.GetDefaultView(_parts);

            Task.Run(() => LoadData());
            Parts.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Part.TypeName)));
        }

        public async Task LoadData()
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();

            //_parts.Clear();
            PartProvider partProvider = _databaseServices.GetRequiredService<PartProvider>();
            SupplierProvider supplierProvider = _databaseServices.GetRequiredService<SupplierProvider>();
            PartTypeProvider partTypeProvider = _databaseServices.GetRequiredService<PartTypeProvider>();

            Task<IEnumerable<Part>> partsTask = partProvider.GetAll();
            Task<IEnumerable<Supplier>> suppliersTask = supplierProvider.GetAll();
            Task<IEnumerable<PartType>> partTypesTask = partTypeProvider.GetAll();

            await Task.WhenAll(partsTask, suppliersTask, partTypesTask);

            IEnumerable<Part> parts = await partsTask;
            IEnumerable<Supplier> suppliers = await suppliersTask;
            IEnumerable<PartType> partTypes = await partTypesTask;

            foreach (Part part in parts)
            {
                //await Task.Delay(200);
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
            //System.Diagnostics.Debug.WriteLine($"LoadDataGetAll Execution Time: {stopwatch.ElapsedTicks} ms");
        }
        //Loading all and then asigning is faster
        public async Task LoadDataOneByOne()
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();

            _parts.Clear();
            PartProvider partProvider = _databaseServices.GetRequiredService<PartProvider>();
            SupplierProvider supplierProvider = _databaseServices.GetRequiredService<SupplierProvider>();
            PartTypeProvider partTypeProvider = _databaseServices.GetRequiredService<PartTypeProvider>();

            IEnumerable<Part> parts = await partProvider.GetAll();

            foreach (Part part in parts)
            {
                Task<Supplier> supplierTask = supplierProvider.GetById(part.SupplierId);
                Task<Supplier> producerTask = supplierProvider.GetById(part.ProducerId);
                Task<PartType> partTypeTask = partTypeProvider.GetById(part.TypeId);

                await Task.WhenAll(supplierTask, producerTask, partTypeTask);

                PartType? partType = await partTypeTask;
                if (partType is not null) part.SetType(partType);

                Supplier? producer = await producerTask;
                if (producer is not null) part.SetProducer(producer);

                Supplier? supplier = await supplierTask;
                if (supplier is not null) part.SetSupplier(supplier);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _parts.Add(part);
                    OnPropertyChanged(nameof(Parts));
                });
            }

            //stopwatch.Stop();
            //System.Diagnostics.Debug.WriteLine($"LoadDataOneByOne Execution Time: {stopwatch.ElapsedTicks} ms");
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
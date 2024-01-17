using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Shopfloor.Features.Admin.Parts.List
{
    public class PartsListViewModel : ViewModelBase
    {
        private bool _isEdit;
        private readonly IServiceProvider _databaseServices;
        private string _errorMassage = string.Empty;
        private string _searchText = string.Empty;
        private readonly ObservableCollection<Part> _parts;
        private Part? _selectedPart;

        #region Parts fields
        private readonly int _id;
        private string? _namePl;
        private string? _nameOriginal;
        private PartType? _type;
        private int? _index;
        private string? _number;
        private string? _details;
        private Supplier? _producer;
        private Supplier? _supplier;
        #endregion

        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public string ErrorMassage
        {
            get => _errorMassage;
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }
        public Visibility HasErrorVisibility
        {
            get => _errorMassage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        public ICollectionView Parts { get; }
        public Part? SelectedPart
        {
            get => _selectedPart;
            set
            {
                if (_selectedPart == value) return;

                _selectedPart = value;
                if (value is null)
                {
                    CleanForm();
                }
                else
                {
                    NamePl = value.NamePl;
                    NameOriginal = value.NameOriginal;
                    Type = value.Type;
                    Index = value.Index;
                    Number = value.Number;
                    Details = value.Details;
                    Producer = value.Producer;
                    Supplier = value.Supplier;

                    IsEdit = true;
                    ErrorMassage = string.Empty;
                }

                OnPropertyChanged(nameof(SelectedPart));
            }
        }

        #region Parts properties
        public int Id => _id;
        public string? NamePl
        {
            get => _namePl;
            set
            {
                _namePl = value;
                OnPropertyChanged(nameof(NamePl));
            }
        }
        public string? NameOriginal
        {
            get => _nameOriginal;
            set
            {
                _nameOriginal = value;
                OnPropertyChanged(nameof(NameOriginal));
            }
        }
        public PartType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public int? Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        public string? Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string? Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        public Supplier? Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }
        public Supplier? Supplier
        {
            get => _supplier;
            set
            {
                _supplier = value;
                OnPropertyChanged(nameof(Supplier));
            }
        }
        #endregion



        public PartsListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _parts = new();

            Parts = CollectionViewSource.GetDefaultView(_parts);
            //Parts.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Part.Type.Name)));

            Task.Run(() => LoadData());
            Parts.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Part.TypeName)));
        }

        public async Task LoadData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

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
                    //Parts.Refresh();
                    OnPropertyChanged(nameof(Parts));
                });
            }

            stopwatch.Stop();

            System.Diagnostics.Debug.WriteLine($"LoadDataGetAll Execution Time: {stopwatch.ElapsedTicks} ms");
        }
        //Loading all and then asigning is faster
        public async Task LoadDataOneByOne()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

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

            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine($"LoadDataOneByOne Execution Time: {stopwatch.ElapsedTicks} ms");
        }

        public void CleanForm()
        {
            NamePl = string.Empty;
            NameOriginal = string.Empty;
            //type
            Index = null;
            Number = null;
            Details = string.Empty;
            //producer
            //supplier

            IsEdit = false;
            ErrorMassage = string.Empty;
            SelectedPart = null;
        }
    }
}
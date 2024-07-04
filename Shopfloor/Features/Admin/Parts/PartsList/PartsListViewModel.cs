using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts
{
    internal sealed class PartsListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Part> _parts;
        private readonly IDataStore<Part> _partsStore;
        private readonly IDataStore<PartType> _partTypesStore;
        private readonly SelectedPartStore _selectedPart;
        private readonly IDataStore<Supplier> _suppliersStore;
        private string _searchText = string.Empty;
        public PartsListViewModel(NavigationService navigationService, IDataStore<PartType> partTypeStore, IDataStore<Supplier> suppliersStore, IDataStore<Part> partStore, SelectedPartStore selectedPartStore)
        {
            _parts = [];
            _selectedPart = selectedPartStore;

            AddPartCommand = new NavigationCommand<PartsAddViewModel>(navigationService).Navigate();
            EditPartCommand = new NavigationCommand<PartsEditViewModel>(navigationService).Navigate();

            Parts = CollectionViewSource.GetDefaultView(_parts);

            _partsStore = partStore;
            _suppliersStore = suppliersStore;
            _partTypesStore = partTypeStore;

            Task.Run(LoadData);
            Parts.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Part.PartType.Name)));
        }
        public ICommand AddPartCommand { get; }
        public ICommand EditPartCommand { get; }
        public Visibility IsSelected => SelectedPart is null ? Visibility.Collapsed : Visibility.Visible;
        public ICollectionView Parts { get; }
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
        public Part? SelectedPart
        {
            get => _selectedPart.Part;
            set
            {
                if (_selectedPart.Part == value)
                {
                    return;
                }

                _selectedPart.Part = value;

                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(SelectedPart));
            }
        }
        public Task LoadData()
        {
            List<Part> parts = _partsStore.Data;
            List<Supplier> suppliers = _suppliersStore.Data;
            List<PartType> partTypes = _partTypesStore.Data;

            foreach (Part part in parts)
            {
                PartType? partType = partTypes.Find(pt => pt.Id == part.TypeId);
                if (partType is not null)
                {
                    part.PartType = partType;
                }

                Supplier? producer = suppliers.Find(p => p.Id == part.ProducerId);
                if (producer is not null)
                {
                    part.Producer = producer;
                }

                Supplier? supplier = suppliers.Find(s => s.Id == part.SupplierId);
                if (supplier is not null)
                {
                    part.Supplier = supplier;
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _parts.Add(part);
                    OnPropertyChanged(nameof(Parts));
                });
            }

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
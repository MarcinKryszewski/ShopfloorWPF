using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.Interfaces;
using Shopfloor.Features.Admin.Parts.List;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts.Add
{
    public class PartsAddViewModel : ViewModelBase, IInputForm<Part>
    {
        private readonly ObservableCollection<PartType> _partTypes = new();
        private readonly ObservableCollection<Supplier> _suppliers = new();
        private string _errorMassage = string.Empty;
        private readonly IServiceProvider _mainServices;

        #region modelFields
        private string _namePl = string.Empty;
        private string _nameOriginal = string.Empty;
        private PartType? _type;
        private int? _index;
        private string _number = string.Empty;
        private string _details = string.Empty;
        private Supplier? _producer;
        private Supplier? _supplier;
        #endregion

        public ICollectionView PartTypes { get; }
        public ICollectionView Suppliers { get; }
        public ICollectionView Producers { get; }
        public string ErrorMassage
        {
            get => _errorMassage.Length > 0 ? _errorMassage : "";
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }
        public Visibility HasErrorVisibility => ErrorMassage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;

        #region model properties
        public string NamePl
        {
            get => _namePl;
            set
            {
                _namePl = value;
                OnPropertyChanged(nameof(NamePl));
            }
        }
        public string NameOriginal
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
        public int? TypeId => Type?.Id;
        public int? Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string Details
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
        public int? ProducerId => Producer?.Id;
        public Supplier? Supplier
        {
            get => _supplier;
            set
            {
                _supplier = value;
                OnPropertyChanged(nameof(Supplier));
            }
        }
        public int? SupplierId => Supplier?.Id;
        #endregion

        public ICommand ReturnCommand { get; }
        public ICommand CleanFormCommand { get; }
        public ICommand AddPartCommand { get; }


        public PartsAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;

            ReturnCommand = new NavigateCommand<PartsListViewModel>(_mainServices.GetRequiredService<NavigationService<PartsListViewModel>>());
            CleanFormCommand = new PartCleanFormCommand(this);
            AddPartCommand = new PartAddCommand(this, databaseServices);

            _partTypes = new(_mainServices.GetRequiredService<PartTypesStore>().Data);
            _suppliers = new(_mainServices.GetRequiredService<SuppliersStore>().Data);

            PartTypes = CollectionViewSource.GetDefaultView(_partTypes);
            Suppliers = CollectionViewSource.GetDefaultView(_suppliers);
            Producers = CollectionViewSource.GetDefaultView(_suppliers);
        }

        public void CleanForm()
        {
            NamePl = string.Empty;
            NameOriginal = string.Empty;
            Type = null;
            Index = null;
            Number = string.Empty;
            Details = string.Empty;
            Producer = null;
            Supplier = null;
            ErrorMassage = string.Empty;
        }

        public bool IsDataValidate(Part inputValue)
        {
            if (inputValue.RequiredInputValue.Length == 0)
            {
                ErrorMassage = "Wprowadź nazwę, nazwę producenta lub indeks";
                return false;
            };
            Part? part = _mainServices.GetRequiredService<PartsStore>().Data.FirstOrDefault(p => p.Index == inputValue.Index);
            if (part is not null)
            {
                ErrorMassage = "Część o podanym indeksie już istnieje";
                return false;
            }
            return true;
        }
    }
}
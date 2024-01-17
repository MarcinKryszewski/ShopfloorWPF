using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Shopfloor.Models;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts.List
{
    public class PartsListViewModel : ViewModelBase
    {
        private bool _isEdit;
        private readonly IServiceProvider _databaseServices;
        private string _errorMassage = string.Empty;
        private string _searchText = string.Empty;

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
            }
        }
        public Visibility HasErrorVisibility
        {
            get => _errorMassage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
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
        }
    }
}
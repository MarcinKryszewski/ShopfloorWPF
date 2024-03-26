using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.PartTypes.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared.ViewModels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.PartTypes.List
{
    internal sealed class PartTypesListViewModel : ViewModelBase, IInputForm<PartType>
    {
        private readonly IServiceProvider _databaseServices;
        private readonly ObservableCollection<PartType> _partTypes = [];
        private readonly PartTypeStore _partTypesStore;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private bool _isEdit;
        private string _name = string.Empty;
        private string _searchText = string.Empty;
        private PartType? _selectedPartType;
        public PartTypesListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();

            AddCommand = new PartTypeAddCommand(this, provider);
            EditCommand = new PartTypeEditCommand(this, provider);
            CleanFormCommand = new CleanFormCommand(this);

            _partTypesStore = _databaseServices.GetRequiredService<PartTypeStore>();
            Task.Run(() => LoadData(_databaseServices));
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand AddCommand { get; }
        public ICommand CleanFormCommand { get; }
        public ICommand EditCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ICollectionView PartTypes => CollectionViewSource.GetDefaultView(_partTypes);
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                PartTypes.Filter = FilterList;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public PartType? SelectedPartType
        {
            get => _selectedPartType;
            set
            {
                _selectedPartType = value;
                if (value is null)
                {
                    IsEdit = false;
                    Name = string.Empty;
                }
                else
                {
                    Name = value.Name;
                    IsEdit = true;
                }

                OnPropertyChanged(nameof(SelectedPartType));
            }
        }
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }
            _propertyErrors[propertyName]?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        public void CleanForm()
        {
            Name = string.Empty;
            IsEdit = false;
            SelectedPartType = null;
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null) return;
            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public bool IsDataValidate => !HasErrors;
        public Task LoadData(IServiceProvider databaseServices)
        {
            Application.Current.Dispatcher.Invoke(_partTypes.Clear);

            List<PartType> partTypes = _partTypesStore.Data;
            foreach (PartType partType in partTypes)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _partTypes.Add(partType);
                    OnPropertyChanged(nameof(PartTypes));
                });
            }
            return Task.CompletedTask;
        }
        public void ReloadData()
        {
            _databaseServices.GetRequiredService<PartTypeStore>().Reload().Wait();
        }
        //Updates the list if value didn't exist, ie. after add
        public async Task UpdateData()
        {
            //await Task.Delay(5000);
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            IEnumerable<PartType> partTypes = await provider.GetAll();
            _ = _databaseServices.GetRequiredService<PartTypeStore>().Reload();
            foreach (PartType partType in partTypes)
            {
                if (_partTypes.FirstOrDefault(s => s.Id == partType.Id) is null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _partTypes.Add(partType);
                        OnPropertyChanged(nameof(PartTypes));
                    });
                }
            }
        }
        //Updates the list if value existed, ie. after edit
        public async Task UpdateData(PartType partTypeToRemove)
        {
            if (partTypeToRemove.Id is null) return;
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            PartType partTypeToAdd = await provider.GetById((int)partTypeToRemove.Id);
            if (_partTypes.FirstOrDefault(s => s.Id == partTypeToRemove.Id) is not null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _partTypes.Remove(partTypeToRemove);
                    _partTypes.Add(partTypeToAdd);
                    OnPropertyChanged(nameof(PartTypes));
                });
            }
        }
        private bool FilterList(object obj)
        {
            if (obj is PartType partType)
            {
                return partType.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        /*public bool IsDataValidate(PartType? partType)
        {
            if (partType is null)
            {
                ErrorMassage = "Wpisz typ części";
                return false;
            }

            if (partType.Name.Length == 0)
            {
                ErrorMassage = "Wpisz typ części";
                return false;
            }

            if (_partTypes.FirstOrDefault(s => string.Equals(s.Name, partType.Name, StringComparison.CurrentCultureIgnoreCase)) is not null)
            {
                ErrorMassage = "Typ części istnieje";
                return false;
            }

            ErrorMassage = string.Empty;
            return true;
        }*/
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}
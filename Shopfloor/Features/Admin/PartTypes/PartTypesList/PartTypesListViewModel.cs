﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Admin.PartTypes.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.PartTypes
{
    internal sealed class PartTypesListViewModel : ViewModelBase, IInputForm<PartType>
    {
        private readonly IProvider<PartType> _partTypeProvider;
        private readonly ObservableCollection<PartType> _partTypes = [];
        private readonly IDataStore<PartType> _partTypesStore;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private bool _isEdit;
        private string _name = string.Empty;
        private string _searchText = string.Empty;
        private PartType? _selectedPartType;
        public PartTypesListViewModel(IProvider<PartType> partTypeProvider, IDataStore<PartType> partTypeStore)
        {
            _partTypeProvider = partTypeProvider;

            AddCommand = new PartTypeAddCommand(this, _partTypeProvider);
            EditCommand = new PartTypeEditCommand(this, _partTypeProvider);
            CleanFormCommand = new CleanFormCommand(this);

            _partTypesStore = partTypeStore;
            Task.Run(LoadData);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand AddCommand { get; }
        public ICommand CleanFormCommand { get; }
        public ICommand EditCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate => !HasErrors;
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
            if (propertyName is null)
            {
                return;
            }

            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public Task LoadData()
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
            _partTypesStore.Reload().Wait();
        }
        //Updates the list if value didn't exist, ie. after add
        public async Task UpdateData()
        {
            //await Task.Delay(5000);
            IEnumerable<PartType> partTypes = await _partTypeProvider.GetAll();
            _partTypesStore.Reload().Wait();
            foreach (PartType partType in from partType in partTypes
                                          where _partTypes.FirstOrDefault(s => s.Id == partType.Id) is null
                                          select partType)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                                {
                                    _partTypes.Add(partType);
                                    OnPropertyChanged(nameof(PartTypes));
                                });
            }
        }
        //Updates the list if value existed, ie. after edit
        public async Task UpdateData(PartType partTypeToRemove)
        {
            if (partTypeToRemove.Id is null)
            {
                return;
            }

            PartType partTypeToAdd = await _partTypeProvider.GetById((int)partTypeToRemove.Id);
            if (_partTypes.FirstOrDefault(s => s.Id == partTypeToRemove.Id) is not null)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
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
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
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
    }
}
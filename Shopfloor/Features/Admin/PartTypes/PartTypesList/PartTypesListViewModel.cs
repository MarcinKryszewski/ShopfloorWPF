﻿using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.PartTypes.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
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

namespace Shopfloor.Features.Admin.PartTypes.List
{
    public class PartTypesListViewModel : ViewModelBase, IInputForm<PartType>
    {
        private string _name = string.Empty;
        private PartType? _selectedPartType;
        private readonly ObservableCollection<PartType> _partTypes = new();
        private bool _isEdit;
        private readonly IServiceProvider _databaseServices;
        private string _errorMassage = string.Empty;
        private string _searchText = string.Empty;
        private readonly PartTypesStore _partTypesStore;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
                    ErrorMassage = string.Empty;
                }

                OnPropertyChanged(nameof(SelectedPartType));
            }
        }

        public ICollectionView PartTypes => CollectionViewSource.GetDefaultView(_partTypes);

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
            get => string.IsNullOrEmpty(_errorMassage) ? string.Empty : _errorMassage;
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }

        public Visibility HasErrorVisibility => string.IsNullOrEmpty(ErrorMassage) ? Visibility.Collapsed : Visibility.Visible;

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

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CleanFormCommand { get; }

        public PartTypesListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();

            AddCommand = new PartTypeAddCommand(this, provider);
            EditCommand = new PartTypeEditCommand(this, provider);
            CleanFormCommand = new CleanFormCommand(this);

            _partTypesStore = _databaseServices.GetRequiredService<PartTypesStore>();
            Task.Run(() => LoadData(_databaseServices));
        }

        public async Task LoadData(IServiceProvider databaseServices)
        {
            List<Task> tasks = new();
            Application.Current.Dispatcher.Invoke(_partTypes.Clear);
            if (!_partTypesStore.IsLoaded) tasks.Add(LoadPartTypes());

            if (tasks.Count > 0) await Task.WhenAll(tasks);

            IEnumerable<PartType> partTypes = _partTypesStore.Data;
            foreach (PartType partType in partTypes)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _partTypes.Add(partType);
                    OnPropertyChanged(nameof(PartTypes));
                });
            }
        }

        public Task LoadPartTypes()
        {
            _partTypesStore.Load();
            return Task.CompletedTask;
        }

        //Updates the list if value didn't exist, ie. after add
        public async Task UpdateData()
        {
            //await Task.Delay(5000);
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            IEnumerable<PartType> partTypes = await provider.GetAll();
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

        public void CleanForm()
        {
            Name = string.Empty;
            IsEdit = false;
            ErrorMassage = string.Empty;
            SelectedPartType = null;
        }

        private bool FilterList(object obj)
        {
            if (obj is PartType partType)
            {
                return partType.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public bool IsDataValidate(PartType? partType)
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
        }

        public void ReloadData()
        {
            _databaseServices.GetRequiredService<PartTypesStore>().Load();
        }
    }
}
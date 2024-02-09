﻿using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandPartsList;
using Shopfloor.Features.Mechanic.Errands.ErrandsList;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
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

namespace Shopfloor.Features.Mechanic.Errands.ErrandsEdit
{
    internal sealed partial class ErrandsEditViewModel : ViewModelBase
    {
        private readonly IServiceProvider _databaseServices;
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        private readonly ErrandValidation _errandValidation;
        private readonly ObservableCollection<Machine> _machines = [];
        private readonly IServiceProvider _mainServices;
        private readonly SelectedErrandStore _selectedErrand;
        private readonly ObservableCollection<User> _users = [];
        private ErrandDTO _errandDTO = new();
        public ErrandsEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            _selectedErrand = mainServices.GetRequiredService<SelectedErrandStore>();

            EditErrandCommand = new ErrandEditCommand(this, _databaseServices, userServices.GetRequiredService<CurrentUserStore>(), _selectedErrand);
            ReturnCommand = new NavigateCommand<ErrandsListViewModel>(_mainServices.GetRequiredService<NavigationService<ErrandsListViewModel>>());
            PrioritySetCommand = new PrioritySetCommand(this);
            ShowPartsListCommand = new ErrandsShowPartsList(this, _mainServices);

            _errandValidation = new(this);

            Task.Run(LoadData);
        }
        public ICommand EditErrandCommand { get; }
        public ErrandDTO ErrandDTO
        {
            get => _errandDTO;
            private set
            {
                _errandDTO = value;
                OnPropertyChanged(nameof(SapNumber));
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(SelectedMachine));
                OnPropertyChanged(nameof(SelectedResponsible));
                OnPropertyChanged(nameof(SelectedType));
                OnPropertyChanged(nameof(TaskDescription));
                OnPropertyChanged(nameof(SapNumber));
            }
        }
        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICommand ReturnCommand { get; }
        public string? SapNumber
        {
            get => _errandDTO.SapNumber;
            set
            {
                _errandDTO.SapNumber = value;
                OnPropertyChanged(nameof(SapNumber));
            }
        }
        public DateTime? SelectedDate
        {
            get => _errandDTO.ExpectedDate;
            set
            {
                _errandDTO.ExpectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public Machine? SelectedMachine
        {
            get => _errandDTO.Machine;
            set
            {
                string myName = nameof(SelectedMachine);
                _errandValidation.ValidateMachine(myName, value);
                _errandDTO.Machine = value;
                if (value != null) _selectedErrand.MachineId = value.Id;
                OnPropertyChanged(myName);
            }
        }
        public User? SelectedResponsible
        {
            get => _errandDTO.Responsible;
            set
            {
                _errandDTO.Responsible = value;
                OnPropertyChanged(nameof(SelectedResponsible));
            }
        }
        public ErrandType? SelectedType
        {
            get => _errandDTO.ErrandType;
            set
            {
                string myName = nameof(SelectedType);
                _errandValidation.ValidateType(myName, value);
                _errandDTO.ErrandType = value;
                OnPropertyChanged(myName);
            }
        }
        public string? TaskDescription
        {
            get => _errandDTO.Description;
            set
            {
                string myName = nameof(TaskDescription);
                _errandValidation.ValidateDescription(myName, value);
                _errandDTO.Description = value;
                OnPropertyChanged(myName);
            }
        }
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);
        public async Task RefreshData()
        {
            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            await errandStore.Reload();
            RefreshLists();
        }
        private void ClearLists()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _selectedErrand.ErrandParts.Clear();
                _selectedErrand.ErrandId = null;
                _selectedErrand.MachineId = null;

                _errandTypes.Clear();
                _users.Clear();
                _machines.Clear();
            });
        }
        private async Task FillLists(MachineStore machineStore, UserStore userStore, ErrandTypeStore errandTypeStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillMachinesList(machineStore));
            tasks.Add(FillUsersList(userStore));
            tasks.Add(FillTypesList(errandTypeStore));
            await Task.WhenAll(tasks);
        }
        private Task FillMachinesList(MachineStore machineStore)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (Machine machine in machineStore.Data)
                {
                    _machines.Add(machine);
                }
            });
            return Task.CompletedTask;
        }
        private Task FillTypesList(ErrandTypeStore errandTypeStore)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (ErrandType type in errandTypeStore.Data)
                {
                    _errandTypes.Add(type);
                }
            });
            return Task.CompletedTask;
        }
        private Task FillUsersList(UserStore userStore)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (User user in userStore.Data)
                {
                    _users.Add(user);
                }
            });
            return Task.CompletedTask;
        }
        private async Task LoadData()
        {
            ClearLists();

            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            ErrandTypeStore errandTypeStore = _databaseServices.GetRequiredService<ErrandTypeStore>();

            await LoadStoresData(machineStore, userStore, errandTypeStore);
            await FillLists(machineStore, userStore, errandTypeStore);

            RefreshLists();
            SetupForm(errandTypeStore);
        }

        private void SetupForm(ErrandTypeStore errandTypeStore)
        {
            Errand? errand = _selectedErrand.SelectedErrand;
            if (errand == null) return;

            SelectedType = errandTypeStore.Data.First((t) => t.Id == errand.ErrandTypeId);
            return;
        }

        private Task LoadErrandTypes(ErrandTypeStore errandTypeStore)
        {
            errandTypeStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadMachines(MachineStore machineStore)
        {
            machineStore.Load();
            return Task.CompletedTask;
        }
        private async Task LoadStoresData(MachineStore machineStore, UserStore userStore, ErrandTypeStore errandTypeStore)
        {
            List<Task> tasks = [];
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!errandTypeStore.IsLoaded) tasks.Add(LoadErrandTypes(errandTypeStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task LoadUsers(UserStore userStore)
        {
            userStore.Load();
            return Task.CompletedTask;
        }
        private void RefreshLists()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Machines.Refresh();
                Users.Refresh();
                ErrandTypes.Refresh();
            });
        }
    }
    internal sealed partial class ErrandsEditViewModel : IInputForm<Errand>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate
        {
            get
            {
                _errandValidation.ValidateMachine(nameof(SelectedMachine), SelectedMachine);
                _errandValidation.ValidateType(nameof(SelectedType), SelectedType);
                _errandValidation.ValidateDescription(nameof(TaskDescription), TaskDescription);
                return !HasErrors;
            }
        }
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? value))
            {
                value = [];
                _propertyErrors.Add(propertyName, value);
            }
            value?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        public void CleanForm()
        {
            ErrandDTO = new();
        }
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            Task.Run(errandStore.Reload);
            RefreshLists();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
    internal sealed partial class ErrandsEditViewModel : IErrandPriority
    {
        public ICommand PrioritySetCommand { get; }
        public string SelectedPriority
        {
            get => _errandDTO.Priority ?? "C";
            set
            {
                _errandDTO.Priority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }
    }
    internal sealed partial class ErrandsEditViewModel : IPartsList
    {
        private ErrandPartsListViewModel? _partsList;
        public Visibility IsPartsListVisible => PartsList == null ? Visibility.Visible : Visibility.Collapsed;
        public ErrandPartsListViewModel? PartsList
        {
            get => _partsList;
            set
            {
                _partsList = value;
                OnPropertyChanged(nameof(PartsList));
                OnPropertyChanged(nameof(IsPartsListVisible));
            }
        }
        public ICommand ShowPartsListCommand { get; }
    }
}
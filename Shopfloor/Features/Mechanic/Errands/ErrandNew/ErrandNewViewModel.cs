using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandNew;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed partial class ErrandNewViewModel : ViewModelBase
    {
        private Errand _errand;
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        private readonly ObservableCollection<Machine> _machines = [];
        private readonly ObservableCollection<User> _users = [];
        private readonly SelectedErrandStore _selectedErrand;
        private readonly ErrandValidation _errandValidation;
        private readonly MachineStore _machineStore;
        private readonly UserStore _userStore;
        private readonly ErrandTypeStore _errandTypeStore;

        public ErrandNewViewModel(ErrandPartsListViewModel errandPartsListViewModel, NavigationService navigationService, SelectedErrandStore selectedErrandStore, ICurrentUserStore currentUserStore, MachineStore machineStore, ErrandTypeStore errandTypeStore, UserStore userStore, ErrandProvider errandProvider, ErrandPartProvider errandPartProvider, ErrandStatusProvider errandStatusProvider, ErrandPartStatusProvider errandPartStatusProvider, ErrandPartStatusStore errandPartStatusStore, ErrandPartStore errandPartStore, ErrandStatusStore errandStatusStore, ErrandStore errandStore)
        {
            _selectedErrand = selectedErrandStore;

            _errand = new()
            {
                CreatedById = (int)currentUserStore.User!.Id!,
            };

            NewErrandCommand = new ErrandNewCommand(this, currentUserStore, _selectedErrand)
            {
                ErrandPartProvider = errandPartProvider,
                ErrandPartStatusProvider = errandPartStatusProvider,
                ErrandProvider = errandProvider,
                ErrandStatusProvider = errandStatusProvider,
                ErrandPartStatusStore = errandPartStatusStore,
                ErrandPartStore = errandPartStore,
                ErrandStatusStore = errandStatusStore,
                ErrandStore = errandStore
            };
            ReturnCommand = new NavigationCommand<ErrandsListViewModel>(navigationService).Navigate();
            PrioritySetCommand = new PrioritySetCommand(this);
            //ShowPartsListCommand = new ErrandsShowPartsList(this, errandPartsListViewModel);

            _errandValidation = new(this);

            Task.Run(LoadData);
            _machineStore = machineStore;
            _errandTypeStore = errandTypeStore;
            _userStore = userStore;
        }

        public Errand Errand
        {
            get => _errand;
            private set
            {
                _errand = value;
                OnPropertyChanged(nameof(SapNumber));
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(SelectedMachine));
                OnPropertyChanged(nameof(SelectedResponsible));
                OnPropertyChanged(nameof(SelectedType));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(SapNumber));
            }
        }

        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);

        public ICommand NewErrandCommand { get; }
        public ICommand ReturnCommand { get; }

        public string? SapNumber
        {
            get => _errand.SapNumber;
            set
            {
                _errand.SapNumber = value;
                OnPropertyChanged(nameof(SapNumber));
            }
        }
        public DateTime? SelectedDate
        {
            get => _errand.ExpectedDate;
            set
            {
                _errand.ExpectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public Machine? SelectedMachine
        {
            get => _errand.Machine;
            set
            {
                string myName = nameof(SelectedMachine);
                _errandValidation.ValidateMachine(myName, value);
                _errand.Machine = value;
                if (value != null) _selectedErrand.MachineId = value.Id;
                OnPropertyChanged(myName);
            }
        }
        public User? SelectedResponsible
        {
            get => _errand.Responsible;
            set
            {
                _errand.Responsible = value;
                OnPropertyChanged(nameof(SelectedResponsible));
            }
        }
        public ErrandType? SelectedType
        {
            get => _errand.Type;
            set
            {
                string myName = nameof(SelectedType);
                _errandValidation.ValidateType(myName, value);
                _errand.Type = value;
                OnPropertyChanged(myName);
            }
        }
        public string? Description
        {
            get => _errand.Description;
            set
            {
                if (value == null) return;
                string myName = nameof(Description);
                _errandValidation.ValidateDescription(myName, value);
                _errand.Description = value;
                OnPropertyChanged(myName);
            }
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

            //await LoadStoresData(_machineStore, _userStore, _errandTypeStore);
            await FillLists(_machineStore, _userStore, _errandTypeStore);
            RefreshLists();
        }
        /*private async Task LoadStoresData(MachineStore machineStore, UserStore userStore, ErrandTypeStore errandTypeStore)
        {
            List<Task> tasks = [];
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!errandTypeStore.IsLoaded) tasks.Add(LoadErrandTypes(errandTypeStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }*/
        private async Task FillLists(MachineStore machineStore, UserStore userStore, ErrandTypeStore errandTypeStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillMachinesList(machineStore));
            tasks.Add(FillUsersList(userStore));
            tasks.Add(FillTypesList(errandTypeStore));
            await Task.WhenAll(tasks);
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
        private void RefreshLists()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Machines.Refresh();
                Users.Refresh();
                ErrandTypes.Refresh();
            });
        }
        // private Task LoadErrandTypes(ErrandTypeStore errandTypeStore)
        // {
        //     errandTypeStore.Load();
        //     return Task.CompletedTask;
        // }
        // private Task LoadMachines(MachineStore machineStore)
        // {
        //     machineStore.Load();
        //     return Task.CompletedTask;
        // }
        // private Task LoadUsers(UserStore userStore)
        // {
        //     userStore.Load();
        //     return Task.CompletedTask;
        // }
    }
    internal sealed partial class ErrandNewViewModel : IInputForm<Errand>
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
                _errandValidation.ValidateDescription(nameof(Description), Description);
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
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        public void CleanForm()
        {
            Errand = new();
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null) return;
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
    }
    internal sealed partial class ErrandNewViewModel : IErrandPriority
    {
        public string SelectedPriority
        {
            get => _errand.Priority ?? "C";
            set
            {
                _errand.Priority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }
        public ICommand PrioritySetCommand { get; }
    }
    internal sealed partial class ErrandNewViewModel : IPartsList
    {
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
        public Visibility IsPartsListVisible => PartsList == null ? Visibility.Visible : Visibility.Collapsed;
        private ErrandPartsListViewModel? _partsList;
        public ICommand ShowPartsListCommand { get; }
    }
}
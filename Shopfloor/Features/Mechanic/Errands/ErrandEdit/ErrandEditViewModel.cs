using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services;
using Shopfloor.Services.NavigationServices;
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

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed partial class ErrandEditViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        //private readonly ErrandValidation _errandValidation;
        private readonly ObservableCollection<Machine> _machines = [];
        private readonly ErrandPartsListViewModel _errandPartsListViewModel;
        private readonly SelectedErrandStore _selectedErrand;
        private readonly IDataStore<Errand> _errandStore;
        private readonly IDataStore<Machine> _machineStore;
        private readonly IDataStore<User> _userStore;
        private readonly IDataStore<ErrandType> _errandTypeStore;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly IDataStore<Part> _partStore;
        private readonly ObservableCollection<User> _users = [];
        private ErrandDTO _errandDTO = new();
        public ErrandEditViewModel(
            INavigationCommand<ErrandsListViewModel> navigateService,
            ErrandPartsListViewModel errandPartsListViewModel,
            StoreRepository stores,
            IProvider<Errand> errandProvider,
            IProvider<ErrandPart> errandPartProvider,
            IProvider<ErrandStatus> errandStatusProvider,
            IProvider<ErrandPartStatus> errandPartStatusProvider)
        {
            _errandPartsListViewModel = errandPartsListViewModel;
            _selectedErrand = stores.SelectedErrand;
            _errandStore = stores.Errand;
            _machineStore = stores.Machine;
            _userStore = stores.User;
            _errandTypeStore = stores.ErrandType;
            _errandPartStore = stores.ErrandPart;
            _partStore = stores.Part;

            EditErrandCommand = new ErrandEditCommand(this, stores.CurrentUser, _selectedErrand, errandProvider, errandPartProvider, errandStatusProvider, stores.ErrandPart, errandPartStatusProvider);
            ReturnCommand = navigateService.Navigate();
            PrioritySetCommand = new PrioritySetCommand(this);
            ShowPartsListCommand = new ErrandsShowPartsList(this, _errandPartsListViewModel);

            //_errandValidation = new(this);

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
        public bool PrioA { get; set; }
        public bool PrioB { get; set; }
        public bool PrioC { get; set; }
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
                //_errandValidation.ValidateMachine(myName, value);
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
                //_errandValidation.ValidateType(myName, value);
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
                //_errandValidation.ValidateDescription(myName, value);
                _errandDTO.Description = value;
                OnPropertyChanged(myName);
            }
        }
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);
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
        private async Task FillLists()
        {
            List<Task> tasks = [];
            tasks.Add(FillMachinesList());
            tasks.Add(FillUsersList());
            tasks.Add(FillTypesList());
            tasks.Add(FillPartsList());
            await Task.WhenAll(tasks);
        }
        private Task FillPartsList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _selectedErrand.ErrandParts.Clear();
                foreach (ErrandPart part in _errandPartStore.Data)
                {
                    if (part.ErrandId == _selectedErrand.SelectedErrand?.Id)
                    {
                        part.Part = _partStore.Data.First(p => p.Id == part.PartId);
                        _selectedErrand.ErrandParts.Add(part);
                    }
                }
            });
            return Task.CompletedTask;
        }
        private Task FillMachinesList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (Machine machine in _machineStore.Data)
                {
                    _machines.Add(machine);
                }
            });
            return Task.CompletedTask;
        }
        private Task FillTypesList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (ErrandType type in _errandTypeStore.Data)
                {
                    _errandTypes.Add(type);
                }
            });
            return Task.CompletedTask;
        }
        private Task FillUsersList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (User user in _userStore.Data)
                {
                    _users.Add(user);
                }
            });
            return Task.CompletedTask;
        }
        private async Task LoadData()
        {
            ClearLists();

            await FillLists();

            RefreshLists();
            SetupForm(_errandTypeStore, _machineStore, _errandPartStore);
        }

        private void SetupForm(IDataStore<ErrandType> errandTypeStore, IDataStore<Machine> machineStore, IDataStore<ErrandPart> errandPartStore)
        {
            Errand? errand = _selectedErrand.SelectedErrand;
            if (errand == null) return;

            SelectedType = errandTypeStore.Data.First((t) => t.Id == errand.TypeId);
            SelectedMachine = machineStore.Data.First((m) => m.Id == errand.MachineId);
            SelectedDate = errand.ExpectedDate;
            SapNumber = errand.SapNumber;
            SelectedResponsible = errand.Responsible;
            SelectedPriority = errand.Priority ?? "C";
            TaskDescription = errand.Description;
            SetupPriority(errand);
            SetupParts();
        }
        public void SetupParts()
        {
            if (_selectedErrand.ErrandParts.Count > 0) PartsList = _errandPartsListViewModel;
        }
        private void SetupPriority(Errand errand)
        {
            switch (errand.Priority)
            {
                case "A":
                    PrioA = true;
                    OnPropertyChanged(nameof(PrioA));
                    break;
                case "B":
                    PrioB = true;
                    OnPropertyChanged(nameof(PrioB));
                    break;
                case "C":
                default:
                    PrioC = true;
                    OnPropertyChanged(nameof(PrioC));
                    break;
            }
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
    internal sealed partial class ErrandEditViewModel : IInputForm<Errand>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate
        {
            get
            {
                //_errandValidation.ValidateMachine(nameof(SelectedMachine), SelectedMachine);
                //_errandValidation.ValidateType(nameof(SelectedType), SelectedType);
                //_errandValidation.ValidateDescription(nameof(TaskDescription), TaskDescription);
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
        public void ReloadData()
        {
            Task.Run(_errandStore.Reload);
            RefreshLists();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
    internal sealed partial class ErrandEditViewModel : IErrandPriority
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
    internal sealed partial class ErrandEditViewModel : IPartsList
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
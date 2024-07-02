using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandNew;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.BaseClasses;
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
        private readonly IDataStore<Machine> _machineStore;
        private readonly IDataStore<User> _userStore;
        private readonly IDataStore<ErrandType> _errandTypeStore;
        private readonly int _currentUserId;
        private readonly ErrandCreatorData _errandCreatorData;

        public ErrandNewViewModel(
            NavigationService navigationService,
            StoreRepository stores,
            IModelCreatorService<Errand> errandCreator,
            ErrandPartsListViewModel errandPartsListViewModel,
            IModelCreatorService<ErrandPart> partCreator,
            IModelEditorService<ErrandStatus> errandStatusEditorService,
            IModelCreatorService<ErrandStatus> statusCreator)
        {
            _selectedErrand = stores.SelectedErrand;
            _currentUserId = (int)stores.CurrentUser.User!.Id!;

            _errand = new() { CreatedById = _currentUserId, };
            _errandCreatorData = new() { Errand = _errand, UserId = _currentUserId };

            NewErrandCommand = new ErrandNewCommand(
                errandCreator,
                partCreator,
                errandStatusEditorService,
                statusCreator);
            ReturnCommand = new NavigationCommand<ErrandsListViewModel>(navigationService).Navigate();
            PrioritySetCommand = new PrioritySetCommand(this);

            ShowPartsListCommand = new ErrandsShowPartsList(this, errandPartsListViewModel);

            NewErrandCommand.ErrandCreated += OnErrandCreated;

            Task.Run(LoadData);
            _machineStore = stores.Machine;
            _errandTypeStore = stores.ErrandType;
            _userStore = stores.User;
        }
        private void OnErrandCreated() => CleanForm();
        public Errand Errand
        {
            get => _errand;
            private set
            {
                _errand = value;
                OnPropertyChanged(nameof(Errand));
            }
        }
        public ErrandCreatorData ErrandCreator => _errandCreatorData;
        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);

        public ErrandNewCommand NewErrandCommand { get; }
        public ICommand ReturnCommand { get; }
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
        }
        private async Task FillLists()
        {
            List<Task> tasks = [];
            tasks.Add(FillMachinesList());
            tasks.Add(FillUsersList());
            tasks.Add(FillTypesList());
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
                //_errandValidation.ValidateMachine(nameof(SelectedMachine), SelectedMachine);
                //_errandValidation.ValidateType(nameof(SelectedType), SelectedType);
                //_errandValidation.ValidateDescription(nameof(Description), Description);
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
            Errand = new()
            {
                CreatedById = _currentUserId
            };
            _errandCreatorData.Errand = Errand;
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
                if (_partsList is not null) _partsList.ErrandData = _errandCreatorData;
                OnPropertyChanged(nameof(PartsList));
                OnPropertyChanged(nameof(IsPartsListVisible));
            }
        }
        public Visibility IsPartsListVisible => PartsList == null ? Visibility.Visible : Visibility.Collapsed;
        private ErrandPartsListViewModel? _partsList;
        public ICommand ShowPartsListCommand { get; }
    }
}
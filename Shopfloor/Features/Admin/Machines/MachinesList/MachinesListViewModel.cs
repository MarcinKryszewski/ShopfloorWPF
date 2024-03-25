using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
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

namespace Shopfloor.Features.Admin.Machines.List
{
    internal sealed partial class MachinesListViewModel
    {
        private readonly IServiceProvider _databaseServices;
        //treeview needs separate collection, which has only root nodes in it
        //this one is for combobox
        private readonly ObservableCollection<Machine> _machines;
        //this one is for treeview
        private readonly List<Machine> _machinesAll;
        private int? _id;
        private bool _isEdit;
        private string _machineName = string.Empty;
        private string _machineNumber = string.Empty;
        private string _sapNumber = string.Empty;
        private string _machineSearchText = string.Empty;
        private readonly MachineStore _machineStore;
        private int _parentId;
        private Machine? _selectedMachine;
        private Machine? _selectedParent;
        private readonly MachineValidation _machineValidation;
        public MachinesListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;

            _machines = [];
            _machinesAll = [];

            IsEdit = false;

            _machineStore = _databaseServices.GetRequiredService<MachineStore>();
            Task.Run(LoadData);

            MachineProvider provider = databaseServices.GetRequiredService<MachineProvider>();

            MachineDeleteCommand = new MachineDeleteCommand();
            MachineAddCommand = new MachineAddCommand(this, provider);
            MachineEditCommand = new MachineEditCommand(this, provider);
            MachineSetParentCommand = new MachineSetParentCommand(this);
            MachineSetCurrentCommand = new MachineSetCurrentCommand(this);
            CleanCommand = new CleanFormCommand(this);
            MachineSelectedCommand = new MachineSelectedCommand();

            _machineValidation = new(this);
        }
        public ICommand CleanCommand { get; }
        public int? Id => _id;
        public string MachineName
        {
            get => _machineName;
            set
            {
                string myName = nameof(MachineName);
                _machineValidation.ValidateName(value, myName);
                _machineName = value;
                OnPropertyChanged(myName);
            }
        }
        public string MachineNumber
        {
            get => _machineNumber;
            set
            {
                _machineNumber = value;
                OnPropertyChanged(nameof(MachineNumber));
            }
        }
        public string SapNumber
        {
            get => _sapNumber;
            set
            {
                _sapNumber = value;
                OnPropertyChanged(nameof(SapNumber));
            }
        }
        public int ParentId
        {
            get => _parentId;
            set
            {
                _parentId = value;
                OnPropertyChanged(nameof(ParentId));
            }
        }
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public ICommand MachineAddCommand { get; }
        public ICommand MachineDeleteCommand { get; }
        public ICommand MachineEditCommand { get; }
        public ObservableCollection<Machine> Machines => _machines;
        public string MachineSearchText
        {
            get => _machineSearchText;
            set
            {
                _machineSearchText = value;

                if (string.IsNullOrEmpty(value))
                {
                    MachinesList.Filter = null;
                }
                else MachinesList.Filter = FilterMachines;

                OnPropertyChanged(nameof(MachineSearchText));
            }
        }
        public ICommand MachineSelectedCommand { get; }
        public ICommand MachineSetCurrentCommand { get; }
        public ICommand MachineSetParentCommand { get; }
        public ICollectionView MachinesList => CollectionViewSource.GetDefaultView(_machinesAll);
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                if (value is null) return;
                if (MachinesList.Filter is not null) MachinesList.Filter = null;
                _id = value.Id;
                MachineName = value.Name;
                MachineNumber = value.Number ?? string.Empty;
                SapNumber = value?.SapNumber ?? string.Empty;
                _selectedMachine = value;
                IsEdit = true;

                OnPropertyChanged(nameof(MachineSearchText));
                OnPropertyChanged(nameof(SelectedMachine));
            }
        }
        public Machine? SelectedParent
        {
            get => _selectedParent;
            set
            {
                string myName = nameof(SelectedParent);
                _selectedParent = value;
                if (_selectedParent != null) _machineValidation.ValidateParent(_selectedParent.Id, myName, Id);
                OnPropertyChanged(nameof(MachineSearchText));
                OnPropertyChanged(myName);
            }
        }
        public void AddToList(Machine machine)
        {
            _machinesAll.Add(machine);
            MachinesList.Refresh();

            if (machine.ParentId is null)
            {
                AddRoot(machine);
                return;
            }
            AddChild(machine, _machinesAll);

            _machines.Clear();
            foreach (Machine item in _machinesAll)
            {
                if (item.ParentId is null) AddRoot(item);
            }
        }
        public void UpdateList()
        {
            Task.Run(LoadData);
        }
        private static void AddChild(Machine machine, List<Machine> machinesList)
        {
            Machine? machineParent = machinesList.FirstOrDefault(m => m.Id == machine.ParentId);
            if (machineParent is null) return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                machineParent.AddChild(machine);
            });
        }
        private void AddRoot(Machine machine)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _machines.Add(machine);
            });
        }
        private bool FilterMachines(object obj)
        {
            if (obj is Machine machine)
            {
                return machine.SearchValue.Contains(_machineSearchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        private Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _machines.Clear();
                _machinesAll.Clear();
            });

            //List<Task> tasks = [];
            //if (!_machineStore.IsLoaded) tasks.Add(LoadMachines());
            //if (tasks.Count > 0) await Task.WhenAll(tasks);

            List<Machine> machines = _machineStore.GetData(true);

            foreach (Machine machine in machines)
            {
                _machinesAll.Add(machine);

                if (machine.ParentId is null)
                {
                    AddRoot(machine);
                    continue;
                }
                AddChild(machine, _machinesAll);
            }
            MachinesList.Refresh();
            return Task.CompletedTask;
        }
    }
    internal sealed partial class MachinesListViewModel : ViewModelBase
    {
    }
    internal sealed partial class MachinesListViewModel
    {
        public void CleanForm()
        {
            _id = null;
            SelectedMachine = null;
            SelectedParent = null;
            MachineName = string.Empty;
            MachineNumber = string.Empty;
            SapNumber = string.Empty;
            IsEdit = false;

            _propertyErrors.Clear();
            OnErrorsChanged(nameof(MachineName));
            OnErrorsChanged(nameof(SelectedParent));

            //OnPropertyChanged(nameof(IsDataValidate));
        }
        public void ReloadData()
        {
            _databaseServices.GetRequiredService<MachineStore>().Reload().Wait();
        }
    }
    internal sealed partial class MachinesListViewModel : IInputForm<Machine>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
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
        public bool IsDataValidate
        {
            get
            {
                _machineValidation.ValidateName(MachineName, nameof(MachineName));
                _machineValidation.ValidateParent(SelectedParent?.Id, nameof(SelectedParent), Id);
                return !HasErrors;
            }
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
        public bool HasErrors => _propertyErrors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    }
}
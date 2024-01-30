using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using System.Collections;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsNew
{
    internal sealed partial class ErrandsNewViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        private readonly ObservableCollection<User> _users = [];
        private ErrandType? _selectedType;
        private Machine? _selectedMachine;
        private DateTime? _selectedDate;
        private string _sapNumber = string.Empty;
        private User? _selectedResponsible;
        private string _selectedPriority = string.Empty;
        private string _taskDescription = string.Empty;
        public ErrandType? SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                _selectedMachine = value;
                OnPropertyChanged(nameof(SelectedMachine));
            }
        }
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
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
        public User? SelectedResponsible
        {
            get => _selectedResponsible;
            set
            {
                _selectedResponsible = value;
                OnPropertyChanged(nameof(SelectedResponsible));
            }
        }
        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }
        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                OnPropertyChanged(nameof(TaskDescription));
            }
        }
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);
        private readonly ObservableCollection<Machine> _machines = [];
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ErrandsNewViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            NewErrandCommand = new ErrandNewCommand(this, _databaseServices, userServices.GetRequiredService<CurrentUserStore>().User?.Id);

            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _errandTypes.Clear();
                _users.Clear();
                _machines.Clear();
            });

            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            ErrandTypeStore errandTypeStore = _databaseServices.GetRequiredService<ErrandTypeStore>();

            List<Task> tasks = [];
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!errandTypeStore.IsLoaded) tasks.Add(LoadErrandTypes(errandTypeStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            tasks.Clear();

            tasks.Add(FillMachinesList(machineStore));
            tasks.Add(FillUsersList(userStore));
            tasks.Add(FillTypesList(errandTypeStore));
            await Task.WhenAll(tasks);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Machines.Refresh();
                Users.Refresh();
                ErrandTypes.Refresh();
            });

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
        private Task LoadMachines(MachineStore machineStore)
        {
            machineStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadUsers(UserStore userStore)
        {
            userStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrandTypes(ErrandTypeStore errandTypeStore)
        {
            errandTypeStore.Load();
            return Task.CompletedTask;
        }
        public ICommand NewErrandCommand { get; }
    }
    internal sealed partial class ErrandsNewViewModel : IInputForm<Errand>
    {
        public bool IsDataValidate => !HasErrors;
        public bool HasErrors => false;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public void AddError(string propertyName, string errorMassage)
        {
            throw new NotImplementedException();
        }
        public void CleanForm()
        {
            throw new NotImplementedException();
        }
        public void ClearErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}

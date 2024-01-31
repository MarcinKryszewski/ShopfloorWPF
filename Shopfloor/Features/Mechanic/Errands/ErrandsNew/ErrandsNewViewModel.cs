using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandsList;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsNew
{
    internal sealed partial class ErrandsNewViewModel : ViewModelBase
    {
        private readonly IServiceProvider _databaseServices;
        private readonly ErrandDTO _errandDTO = new();
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        private readonly ObservableCollection<Machine> _machines = [];
        private readonly IServiceProvider _mainServices;
        private readonly ObservableCollection<User> _users = [];
        public ErrandsNewViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            NewErrandCommand = new ErrandNewCommand(this, _databaseServices, userServices.GetRequiredService<CurrentUserStore>());
            ReturnCommand = new NavigateCommand<ErrandsListViewModel>(mainServices.GetRequiredService<NavigationService<ErrandsListViewModel>>());

            Task.Run(LoadData);
        }
        public ErrandDTO ErrandDTO => _errandDTO;
        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ICommand NewErrandCommand { get; }
        public ICommand ReturnCommand { get; }
        public string SapNumber
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
                _errandDTO.Machine = value;
                OnPropertyChanged(nameof(SelectedMachine));
            }
        }
        public string SelectedPriority
        {
            get => _errandDTO.Priority;
            set
            {
                _errandDTO.Priority = value;
                OnPropertyChanged(nameof(SelectedPriority));
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
                _errandDTO.ErrandType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public string TaskDescription
        {
            get => _errandDTO.Description;
            set
            {
                _errandDTO.Description = value;
                OnPropertyChanged(nameof(TaskDescription));
            }
        }
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);
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
        private Task LoadUsers(UserStore userStore)
        {
            userStore.Load();
            return Task.CompletedTask;
        }
    }
    internal sealed partial class ErrandsNewViewModel : IInputForm<Errand>
    {
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => false;
        public bool IsDataValidate => !HasErrors;
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
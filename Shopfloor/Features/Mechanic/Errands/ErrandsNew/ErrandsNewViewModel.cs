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
using Windows.ApplicationModel.UserDataTasks;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsNew
{
    sealed internal class ErrandsNewViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly ObservableCollection<ErrandType> _errandTypes = [];
        public ICollectionView ErrandTypes => CollectionViewSource.GetDefaultView(_errandTypes);
        private readonly ObservableCollection<User> _users = [];
        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);
        private readonly ObservableCollection<Machine> _machines = [];
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machines);
        public ErrandsNewViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

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
            UserStore userStore = _databaseServices.GetRequiredService<UserDataTaskStore>();
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
        }
        private Task FillMachinesList(MachineStore machineStore)
        {
            foreach (Machine machine in machineStore.Data)
            {
                _machines.Add(machine);
            }
            Machines.Refresh();
            return Task.CompletedTask;
        }
        private Task FillUsersList(UserStore userStore)
        {
            foreach (User user in userStore.Data)
            {
                _users.Add(user);
            }
            Users.Refresh();
            return Task.CompletedTask;
        }
        private Task FillTypesList(ErrandTypeStore errandTypeStore)
        {
            foreach (ErrandType type in errandTypeStore.Data)
            {
                _errandTypes.Add(type);
            }
            ErrandTypes.Refresh();
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
    }
}

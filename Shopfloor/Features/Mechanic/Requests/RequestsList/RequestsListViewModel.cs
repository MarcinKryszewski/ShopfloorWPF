using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests.RequestsList
{
    internal sealed class RequestsListViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _parts = [];
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;

        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public RequestsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke
            (() =>
            {
                _parts.Clear();
            });

            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStatusStore partsStatusStore = _databaseServices.GetRequiredService<ErrandPartStatusStore>();

            await LoadStores(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore);
            await FillLists(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore);

            Application.Current.Dispatcher.Invoke
            (() =>
            {
                Parts.Refresh();
            });
        }
        private async Task FillLists(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillPartList(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task LoadStores(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore)
        {
            List<Task> tasks = [];
            if (!errandStore.IsLoaded) tasks.Add(LoadErrands(errandStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (!partsStore.IsLoaded) tasks.Add(LoadParts(partsStore));
            if (!partsStatusStore.IsLoaded) tasks.Add(LoadPartsStatus(partsStatusStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadUsers(UserStore userStore)
        {
            userStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrands(ErrandStore errandStore)
        {
            errandStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadMachines(MachineStore machineStore)
        {
            machineStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadParts(PartsStore partsStore)
        {
            partsStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadPartsStatus(ErrandPartStatusStore partsStatusStore)
        {
            partsStatusStore.Load();
            return Task.CompletedTask;
        }
        private Task FillPartList(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.Part = partsStore.Data.FirstOrDefault(p => p.Id == errandPart.PartId);
                IEnumerable<ErrandPartStatus> partStatuses = partsStatusStore.Data.Where(ps => ps.ErrandPartId == errandPart.Id);
                foreach (ErrandPartStatus partStatus in partStatuses)
                {
                    errandPart.StatusList.Add(partStatus);
                }
                _parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
    }
}
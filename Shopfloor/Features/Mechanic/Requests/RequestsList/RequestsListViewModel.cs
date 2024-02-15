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
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests.RequestsList
{
    internal sealed class RequestsListViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _parts = [];
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        public ErrandPart? SelectedErrandPart { get; set; }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public RequestsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_parts.Clear);

            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStatusStore partsStatusStore = _databaseServices.GetRequiredService<ErrandPartStatusStore>();
            PartTypesStore partTypesStore = _databaseServices.GetRequiredService<PartTypesStore>();

            await LoadStores(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore, partTypesStore);
            await CombineData(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore, partTypesStore);
            await FillLists(errandPartStore);

            Application.Current.Dispatcher.Invoke(Parts.Refresh);
        }
        private async Task FillLists(ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillPartList(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task CombineErrandPartWithStatus(ErrandPartStore errandPartStore, ErrandPartStatusStore partsStatusStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.StatusList.AddRange(partsStatusStore.Data.Where(p => p.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private Task CombineData(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore, PartTypesStore partTypesStore)
        {
            CombineErrandWithMachine(errandStore, machineStore);
            CombineErrandPartWithErrand(errandStore, errandPartStore);
            CombineErrandPartWithPart(errandPartStore, partsStore);
            CombineErrandPartWithStatus(errandPartStore, partsStatusStore);
            CombinePartWithType(partsStore, partTypesStore);
            CombineErrandPartWithUser(errandPartStore, userStore);
            return Task.CompletedTask;
        }
        private static Task CombineErrandWithMachine(ErrandStore errandStore, MachineStore machineStore)
        {
            foreach (Errand errand in errandStore.Data)
            {
                errand.Machine = machineStore.Data.FirstOrDefault(m => m.Id == errand.MachineId);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithErrand(ErrandStore errandStore, ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.Errand = errandStore.Data.FirstOrDefault(e => e.Id == errandPart.ErrandId);
            }
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithUser(ErrandPartStore errandPartStore, UserStore userStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.OrderedByUser = userStore.Data.FirstOrDefault(u => u.Id == errandPart.OrderedById);
            }
            return Task.CompletedTask;
        }
        private async Task LoadStores(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore, PartTypesStore partTypesStore)
        {
            List<Task> tasks = [];
            if (!errandStore.IsLoaded) tasks.Add(LoadErrands(errandStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (!partsStore.IsLoaded) tasks.Add(LoadParts(partsStore));
            if (!partsStatusStore.IsLoaded) tasks.Add(LoadPartsStatus(partsStatusStore));
            if (!partTypesStore.IsLoaded) tasks.Add(LoadPartTpesStatus(partTypesStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadPartTpesStatus(PartTypesStore partTypesStore)
        {
            partTypesStore.Load();
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
        private Task CombineErrandPartWithPart(ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.Part = partsStore.Data.FirstOrDefault(p => p.Id == errandPart.PartId);
            }
            return Task.CompletedTask;
        }
        private Task CombinePartWithType(PartsStore partsStore, PartTypesStore partTypesStore)
        {
            foreach (Part part in partsStore.Data)
            {
                part.SetType(partTypesStore.Data.FirstOrDefault(type => type.Id == part.TypeId));
            }
            return Task.CompletedTask;
        }
        private Task FillPartList(ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                _parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsList
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly List<Errand> _errands = [];
        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public Errand? SelectedErrand { get; set; }
        public ICommand EditErrandCommand { get; }
        public ErrandsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
            Task.Run(LoadData);
            ErrandsAddNavigateCommand = new NavigateCommand<ErrandsNewViewModel>(_mainServices.GetRequiredService<NavigationService<ErrandsNewViewModel>>());
            EditErrandCommand = new ErrandSetCommand(this, _mainServices);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_errands.Clear);

            ErrandStore errandStore = _databaseServices.GetRequiredService<ErrandStore>();
            UserStore userStore = _databaseServices.GetRequiredService<UserStore>();
            MachineStore machineStore = _databaseServices.GetRequiredService<MachineStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandStatusStore errandStatusStore = _databaseServices.GetRequiredService<ErrandStatusStore>();
            ErrandPartStatusStore partsStatusStore = _databaseServices.GetRequiredService<ErrandPartStatusStore>();
            ErrandTypeStore errandTypeStore = _databaseServices.GetRequiredService<ErrandTypeStore>();

            await LoadStores(errandStore, userStore, machineStore, errandPartStore, partsStore, partsStatusStore, errandTypeStore, errandStatusStore);
            await CombineData(errandStore, userStore, machineStore, errandTypeStore, errandStatusStore, errandPartStore, partsStatusStore);
            await FillErrandList(errandStore);

            Application.Current.Dispatcher.Invoke(Errands.Refresh);
        }
        #region LOAD_DATA
        private async Task LoadStores(ErrandStore errandStore, UserStore userStore, MachineStore machineStore, ErrandPartStore errandPartStore, PartsStore partsStore, ErrandPartStatusStore partsStatusStore, ErrandTypeStore errandTypeStore, ErrandStatusStore errandStatusStore)
        {
            List<Task> tasks = [];
            if (!errandStore.IsLoaded) tasks.Add(LoadErrands(errandStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (!partsStore.IsLoaded) tasks.Add(LoadParts(partsStore));
            if (!partsStatusStore.IsLoaded) tasks.Add(LoadPartsStatus(partsStatusStore));
            if (!errandTypeStore.IsLoaded) tasks.Add(LoadErrandTypes(errandTypeStore));
            if (!errandStatusStore.IsLoaded) tasks.Add(LoadErrandStatusStore(errandStatusStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task LoadErrandStatusStore(ErrandStatusStore errandStatusStore)
        {
            errandStatusStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrandTypes(ErrandTypeStore errandTypeStore)
        {
            errandTypeStore.Load();
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
        #endregion LOAD_DATA
        #region COMBINE_DATA
        private Task CombineData(ErrandStore errandStore, UserStore users, MachineStore machines, ErrandTypeStore types, ErrandStatusStore statuses, ErrandPartStore errandParts, ErrandPartStatusStore partsStatus)
        {
            CombineErrandPartWithStatus(errandParts, partsStatus);
            CombineErrands(errandStore, users, machines, types, statuses, errandParts);
            return Task.CompletedTask;
        }
        private Task CombineErrandPartWithStatus(ErrandPartStore errandPartStore, ErrandPartStatusStore partsStatusStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(partsStatusStore.Data.Where(p => p.ErrandPartId == errandPart.Id));
            }
            return Task.CompletedTask;
        }
        private static Task CombineErrands(ErrandStore errandStore, UserStore users, MachineStore machines, ErrandTypeStore types, ErrandStatusStore statuses, ErrandPartStore errandParts)
        {
            foreach (Errand errand in errandStore.Data)
            {
                errand.Parts.Clear();
                if (errand.OwnerId is not null) errand.Responsible = users.Data.First((u) => u.Id == errand.OwnerId);
                if (errand.CreatedById is not null) errand.CreatedByUser = users.Data.First((u) => u.Id == errand.CreatedById);
                if (errand.MachineId is not null) errand.Machine = machines.Data.First((m) => m.Id == errand.MachineId);
                if (errand.ErrandTypeId is not null) errand.Type = types.Data.First((t) => t.Id == errand.ErrandTypeId);

                foreach (ErrandStatus status in statuses.Data)
                {
                    if (status.ErrandId == errand.Id) errand.AddStatus(status);
                }
                foreach (ErrandPart errandPart in errandParts.Data)
                {
                    if (errandPart.ErrandId == errand.Id) errand.Parts.Add(errandPart);
                }
            }
            return Task.CompletedTask;
        }
        #endregion COMBINE_DATA
        private Task FillErrandList(ErrandStore errandStore)
        {
            _errands.AddRange(from Errand errand in errandStore.Data select errand);
            return Task.CompletedTask;
        }
    }
}
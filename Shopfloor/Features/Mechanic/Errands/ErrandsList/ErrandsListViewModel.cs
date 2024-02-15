using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
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
            ErrandTypeStore errandTypeStore = _databaseServices.GetRequiredService<ErrandTypeStore>();
            ErrandStatusStore errandStatusStore = _databaseServices.GetRequiredService<ErrandStatusStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();

            List<Task> tasks = [];
            if (!errandStore.IsLoaded) tasks.Add(LoadErrands(errandStore));
            if (!userStore.IsLoaded) tasks.Add(LoadUsers(userStore));
            if (!machineStore.IsLoaded) tasks.Add(LoadMachines(machineStore));
            if (!errandTypeStore.IsLoaded) tasks.Add(LoadTypes(errandTypeStore));
            if (!errandStatusStore.IsLoaded) tasks.Add(LoadStatuses(errandStatusStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            tasks.Clear();

            tasks.Add(FillErrandList(errandStore, userStore.Data, machineStore.Data, errandTypeStore.Data, errandStatusStore.Data, errandPartStore.Data));

            if (tasks.Count > 0) await Task.WhenAll(tasks);

            Application.Current.Dispatcher.Invoke(Errands.Refresh);
        }

        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }

        private Task LoadStatuses(ErrandStatusStore errandStatusStore)
        {
            errandStatusStore.Load();
            return Task.CompletedTask;
        }

        private Task LoadTypes(ErrandTypeStore errandTypeStore)
        {
            errandTypeStore.Load();
            return Task.CompletedTask;
        }

        private Task LoadErrands(ErrandStore errandStore)
        {
            errandStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadUsers(UserStore userStore)
        {
            userStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadMachines(MachineStore machineStore)
        {
            machineStore.Load();
            return Task.CompletedTask;
        }
        private Task FillErrandList(ErrandStore errandStore, IEnumerable<User> users, IEnumerable<Machine> machines, IEnumerable<ErrandType> types, IEnumerable<ErrandStatus> statuses, IEnumerable<ErrandPart> errandParts)
        {
            foreach (Errand errand in errandStore.Data)
            {
                if (errand.OwnerId is not null) errand.Responsible = users.First((u) => u.Id == errand.OwnerId);
                if (errand.CreatedById is not null) errand.CreatedByUser = users.First((u) => u.Id == errand.CreatedById);
                if (errand.MachineId is not null) errand.Machine = machines.First((m) => m.Id == errand.MachineId);
                if (errand.ErrandTypeId is not null) errand.Type = types.First((t) => t.Id == errand.ErrandTypeId);

                foreach (ErrandStatus status in statuses)
                {
                    if (status.ErrandId == errand.Id) errand.AddStatus(status);
                }
                foreach (ErrandPart errandPart in errandParts)
                {
                    if (errandPart.ErrandId == errand.Id) errand.Parts.Add(errandPart);
                }

                _errands.Add(errand);
            }
            return Task.CompletedTask;
        }
    }
}
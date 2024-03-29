using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared;
using Shopfloor.Shared.ViewModels;
using ToastNotifications;

namespace Shopfloor.Features.Plannist.PlannistDashboard.PlannistPartsList
{
    internal sealed class PlannistPartsListViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _parts = [];
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly SelectedRequestStore _requestStore;
        private string? _filterText;
        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
        }
        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (string.IsNullOrEmpty(value))
                {
                    Parts.Filter = null;
                }
                else Parts.Filter = FilterParts;
            }
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public PlannistConfirmCommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        public PlannistAbortCommand AbortCommand { get; }
        public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public PlannistPartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            Task.Run(LoadData);

            _requestStore = _mainServices.GetRequiredService<SelectedRequestStore>();
            SelectedRow = null;

            ConfirmCommand = new PlannistConfirmCommand(_requestStore, databaseServices, mainServices.GetRequiredService<Notifier>());
            CancelCommand = new PlannistCancelCommand();
            AbortCommand = new PlannistAbortCommand(_requestStore, databaseServices);
            DetailsCommand = new PlannistDetailsCommand();

            ConfirmCommand.RequestConfirmed += OnRequestChanged;
            AbortCommand.RequestAborted += OnRequestChanged;

            /*PropertyGroupDescription groupDescription = new("Errand");
            Parts.GroupDescriptions.Clear();
            Parts.GroupDescriptions.Add(groupDescription);*/
        }
        private void OnRequestChanged() => Parts.Refresh();
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

            await LoadStores(errandStore, errandPartStore, partsStore);
            await CombineData(errandStore, errandPartStore, partsStore);
            await FillLists(errandPartStore);

            RefreshView();
        }
        private void RefreshView()
        {
            /*PropertyGroupDescription groupDescription = new("Errand");
            Parts.GroupDescriptions.Clear();
            Parts.GroupDescriptions.Add(groupDescription);*/

            Application.Current.Dispatcher.Invoke(() => Parts.Refresh);
            OnPropertyChanged(nameof(Parts));
        }
        private static async Task LoadStores(ErrandStore errandStore, ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            List<Task> tasks = [];
            tasks.Add(DataStore.LoadData(errandStore));
            tasks.Add(DataStore.LoadData(errandPartStore));
            tasks.Add(DataStore.LoadData(partsStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task CombineData(ErrandStore errandStore, ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            List<Task> tasks = [];

            tasks.Add(errandStore.CombineData());
            tasks.Add(errandPartStore.CombineData());
            tasks.Add(partsStore.CombineData());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task FillLists(ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillPartList(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task FillPartList(ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                _parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        private bool FilterParts(object obj)
        {
            if (string.IsNullOrEmpty(_filterText)) return true;
            if (obj is ErrandPart errandPart)
            {
                return errandPart.SearchValue.Contains(_filterText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Manager.OrdersToApprove
{
    internal sealed class OrdersToApproveViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _orders = [];
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
                Orders.Filter = string.IsNullOrEmpty(value) ? null : FilterParts;
            }
        }
        public ICollectionView Orders => CollectionViewSource.GetDefaultView(_orders);
        public ICommand ApproveCommand { get; }
        //public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public OrdersToApproveViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            Task.Run(LoadData);

            _requestStore = _mainServices.GetRequiredService<SelectedRequestStore>();
            SelectedRow = null;

            ApproveCommand = new NavigateCommand<OrderApproveViewModel>(_mainServices.GetRequiredService<NavigationService<OrderApproveViewModel>>());
            //DetailsCommand = new PlannistDetailsCommand();
        }
        private void OnRequestChanged() => Orders.Refresh();
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_orders.Clear);

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

            Application.Current.Dispatcher.Invoke(() =>
            {
                Orders.Refresh();
                System.Diagnostics.Debug.WriteLine("TEST");
            });
        }
        private static async Task LoadStores(ErrandStore errandStore, ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            List<Task> tasks = [];
            tasks.Add(DataStore.LoadData(errandStore));
            tasks.Add(DataStore.LoadData(errandPartStore));
            tasks.Add(DataStore.LoadData(partsStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private static async Task CombineData(ErrandStore errandStore, ErrandPartStore errandPartStore, PartsStore partsStore)
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
            //await Task.Delay(2000);

        }
        private Task FillPartList(ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                if (errandPart.LastStatusText == ErrandPartStatus.Status[1]) _orders.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        private bool FilterParts(object obj)
        {
            if (string.IsNullOrEmpty(_filterText)) return true;
            if (obj is ErrandPart errandPart) return errandPart.SearchValue.Contains(_filterText, StringComparison.InvariantCultureIgnoreCase);
            return false;
        }
    }
}
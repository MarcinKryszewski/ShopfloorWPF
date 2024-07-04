using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Manager.OrdersToApprove
{
    internal sealed class OrdersToApproveViewModel : ViewModelBase
    {
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _requestStore;
        private string? _filterText;
        private List<ErrandPart> _orders = [];
        public OrdersToApproveViewModel(
            NavigationService navigationService,
            SelectedRequestStore selectedRequestStore,
            IDataStore<ErrandPart> errandPartStore,
            ICombinerManager<ErrandPart> errandPartCombiner)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            _errandPartCombiner = errandPartCombiner;
            SelectedRow = null;

            ApproveCommand = new NavigationCommand<OrderApproveViewModel>(navigationService).Navigate();
            //DetailsCommand = new PlannistDetailsCommand();
            Task.Run(LoadData);
        }
        public ICommand ApproveCommand { get; }
        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                Orders.Filter = string.IsNullOrEmpty(value) ? null : FilterParts;
            }
        }
        //public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ICollectionView Orders => CollectionViewSource.GetDefaultView(_orders);
        public ErrandPart? SelectedRow
        {
            get => _requestStore.Request;
            set => _requestStore.Request = value;
        }
        private bool FilterParts(object obj)
        {
            if (string.IsNullOrEmpty(_filterText))
            {
                return true;
            }

            if (obj is ErrandPart errandPart)
            {
                return errandPart.SearchValue.Contains(_filterText, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
        private Task LoadData()
        {
            _errandPartCombiner.CombineAll().Wait();
            _orders = _errandPartStore.Data.Where(part => part.LastStatusText == ErrandPartStatus.Status[1]).ToList();

            Application.Current.Dispatcher.Invoke(Orders.Refresh);

            return Task.CompletedTask;
        }
        private void OnRequestChanged() => Orders.Refresh();
    }
}
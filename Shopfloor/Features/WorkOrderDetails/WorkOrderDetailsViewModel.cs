using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.WorkOrderDetails
{
    internal class WorkOrderDetailsViewModel : ViewModelBase
    {
        private readonly PartsBasketContext _partsBasket;
        public WorkOrderDetailsViewModel(
            ViewModelBaseDependecies dependecies,
            WorkOrderContext store,
            PartsBasketContext partsBasket,
            WorkOrderDetailsRoot root)
        : base(dependecies)
        {
            WorkOder = store.WorkOrder!;
            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
            root.DataChanged += DataChanged;

            _partsBasket = partsBasket;
            _partsBasket.Parts.Clear();

            root.LoadBasket();
        }
        public ICommand WorkOrdersListNavigate { get; }
        public WorkOrderModel WorkOder { get; init; }
        public Visibility IsPartsListVisible => _partsBasket.Parts.Any() ? Visibility.Visible : Visibility.Collapsed;
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_partsBasket.Parts);
        public void DataChanged(object? sender, EventArgs e)
        {
            Parts.Refresh();
        }
    }
}
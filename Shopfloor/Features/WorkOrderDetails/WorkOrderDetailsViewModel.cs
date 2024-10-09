using System;
using System.Collections.ObjectModel;
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

            Parts = partsBasket.Parts;
            Parts.Clear();

            root.LoadBasket();
        }
        public ICommand WorkOrdersListNavigate { get; }
        public WorkOrderModel WorkOder { get; init; }
        public ObservableCollection<WorkOrderPartCreationModel> Parts { get; }
        public void DataChanged(object? sender, EventArgs e)
        {
            // Parts.Refresh();
        }
    }
}
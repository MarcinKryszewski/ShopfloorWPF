using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.WorkOrderDetails
{
    internal class WorkOrderDetailsViewModel : ViewModelBase
    {
        public WorkOrderDetailsViewModel(ViewModelBaseDependecies dependecies, WorkOrderContext store)
        : base(dependecies)
        {
            WorkOder = store.WorkOrder!;
            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
        }
        public ICommand WorkOrdersListNavigate { get; }
        public WorkOrderModel WorkOder { get; init; }
    }
}
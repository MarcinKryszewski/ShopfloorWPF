using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.WorkOrderAddNew.Commands;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.WorkOrderAddNew
{
    internal class WorkOrderAddNewViewModel : ViewModelBase
    {
        private readonly WorkOrderCreateRoot _root;
        private readonly List<LineModel> _lines = [];
        public WorkOrderAddNewViewModel(ViewModelBaseDependecies dependecies, WorkOrderCreateRoot root)
        : base(dependecies)
        {
            _root = root;

            _ = LoadDataAsync();

            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
            WorkOrderCreateCommand = new WorkOrderCreateCommand(Notifier, _root);
        }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public WorkOrderCreationModel WorkOrder { get; set; } = new();
        public ICommand WorkOrderCreateCommand { get; }
        public ICommand WorkOrdersListNavigate { get; }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<LineModel> dataWorkOrder = await _root.GetLines();

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataWorkOrder,
                _lines,
                Lines));

            await Task.WhenAll(tasks);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.WorkOrderEdit.Commands;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.WorkOrderEdit
{
    internal class WorkOrderEditViewModel : ViewModelBase
    {
        private readonly List<LineModel> _lines = [];
        private readonly WorkOrderEditRoot _root;
        public WorkOrderEditViewModel(ViewModelBaseDependecies dependecies, WorkOrderEditRoot root, WorkOrderContext store)
        : base(dependecies)
        {
            _root = root;
            WorkOder = store.ToWorkOrderCreation();
            _ = LoadDataAsync();

            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
            WorkOrderEditCommand = new WorkOrderEditCommand(dependecies.Notifier, root);
        }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public WorkOrderCreationModel WorkOder { get; init; }
        public ICommand WorkOrderEditCommand { get; }
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.WorkOrderAddNew.Commands;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;
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
        private Visibility _isPartsListVisible = Visibility.Collapsed;
        public WorkOrderAddNewViewModel(ViewModelBaseDependecies dependecies, WorkOrderCreateRoot root, PartsBasketContext partsBasket, PartsListViewModel partsListViewModel)
        : base(dependecies)
        {
            _root = root;
            Parts = partsBasket.Parts;

            _ = LoadDataAsync();

            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
            WorkOrderCreateCommand = new WorkOrderCreateCommand(Notifier, _root);
            ShowPartsList = new ShowPartsListCommand(this);
            PartsListViewModel = partsListViewModel;
        }
        public ObservableCollection<PartModel> Parts { get; }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public WorkOrderCreationModel WorkOrder { get; set; } = new();
        public ICommand WorkOrderCreateCommand { get; }
        public ICommand WorkOrdersListNavigate { get; }
        public ICommand ShowPartsList { get; }
        public Visibility IsPartsListVisible
        {
            get => _isPartsListVisible;
            set
            {
                _isPartsListVisible = value;
                OnPropertyChanged(nameof(IsPartsListVisible));
            }
        }
        public PartsListViewModel PartsListViewModel { get; }
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
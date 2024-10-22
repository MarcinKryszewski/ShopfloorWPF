using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.PartsList.Interfaces;
using Shopfloor.Features.WorkOrderAddNew.Commands;
using Shopfloor.Features.WorkOrderEdit.Commands;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.WorkOrderEdit
{
    internal class WorkOrderEditViewModel : ViewModelBase, IViewModelContainingPartsList
    {
        private readonly List<LineModel> _lines = [];
        private readonly WorkOrderEditRoot _root;
        private Visibility _isPartsListVisible = Visibility.Collapsed;
        public WorkOrderEditViewModel(
            ViewModelBaseDependecies dependecies,
            WorkOrderEditRoot root,
            WorkOrderContext store,
            PartsBasketContext partsBasket,
            PartsListViewModel partsListViewModel)
        : base(dependecies)
        {
            _root = root;
            WorkOder = store.ToWorkOrderCreation();

            partsBasket.WorkOrder = store.WorkOrder;
            Parts = partsBasket.Parts;
            Parts.Clear();

            _root.LoadBasket();
            partsBasket.OriginalPartsList = [.. Parts];
            IsPartsListVisible = Parts.Any() ? Visibility.Visible : Visibility.Collapsed;

            _ = LoadDataAsync();

            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(NavigationService).Navigate();
            WorkOrderEditCommand = new WorkOrderEditCommand(dependecies.Notifier, root);
            ShowPartsList = new ShowPartsListCommand(this);
            RemovePartCommand = new RemovePartFromBasketCommand(partsBasket);
            PartsListViewModel = partsListViewModel;
        }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public ObservableCollection<WorkOrderPartCreationModel> Parts { get; }
        public Visibility IsPartsListVisible
        {
            get => _isPartsListVisible;
            set
            {
                _isPartsListVisible = value;
                IsPartsListButtonVisible = value == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged(nameof(IsPartsListVisible));
                OnPropertyChanged(nameof(IsPartsListButtonVisible));
            }
        }
        public Visibility IsPartsListButtonVisible { get; private set; } = Visibility.Visible;
        public PartsListViewModel PartsListViewModel { get; }
        public WorkOrderCreationModel WorkOder { get; init; }
        public ICommand WorkOrderEditCommand { get; }
        public ICommand WorkOrdersListNavigate { get; }
        public ICommand ShowPartsList { get; }
        public ICommand RemovePartCommand { get; }
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
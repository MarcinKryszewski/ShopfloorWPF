using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToastNotifications;

namespace Shopfloor.Features.Plannist
{
    internal sealed class PlannistPartsListViewModel : ViewModelBase
    {
        private readonly List<ErrandPart> _parts = [];
        private readonly SelectedRequestStore _requestStore;
        private readonly ErrandPartStore _errandPartStore;
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
        public PlannistPartsListViewModel(Notifier notifier, SelectedRequestStore selectedRequestStore, ErrandPartStatusProvider errandPartStatusProvider, ErrandPartStore errandPartStore)
        {
            Task.Run(LoadData);

            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            SelectedRow = null;

            ConfirmCommand = new PlannistConfirmCommand(_requestStore, notifier, errandPartStatusProvider);
            CancelCommand = new PlannistCancelCommand();
            AbortCommand = new PlannistAbortCommand(_requestStore, errandPartStatusProvider);
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

            await FillLists(_errandPartStore);

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
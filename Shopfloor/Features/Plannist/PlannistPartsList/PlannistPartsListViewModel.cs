using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Plannist
{
    internal sealed class PlannistPartsListViewModel : ViewModelBase
    {
        private List<ErrandPart> _parts = [];
        private readonly SelectedRequestStore _requestStore;
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandPartCombiner _errandPartCombiner;
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
        public PlannistPartsListViewModel(
            INotifier notifier,
            SelectedRequestStore selectedRequestStore,
            ErrandPartStatusProvider errandPartStatusProvider,
            ErrandPartStore errandPartStore,
            ErrandPartCombiner errandPartCombiner)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            _errandPartCombiner = errandPartCombiner;
            SelectedRow = null;

            LoadData();

            ConfirmCommand = new PlannistConfirmCommand(_requestStore, notifier, errandPartStatusProvider);
            CancelCommand = new PlannistCancelCommand();
            AbortCommand = new PlannistAbortCommand(_requestStore, errandPartStatusProvider);
            DetailsCommand = new PlannistDetailsCommand();

            ConfirmCommand.RequestConfirmed += OnRequestChanged;
            AbortCommand.RequestAborted += OnRequestChanged;
        }
        private void OnRequestChanged() => Parts.Refresh();
        private Task LoadData()
        {
            _errandPartCombiner.Combine().Wait();
            _parts = _errandPartStore.Data;


            Application.Current.Dispatcher.Invoke(Parts.Refresh);

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
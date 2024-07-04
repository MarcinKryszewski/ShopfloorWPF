using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Plannist
{
    internal sealed class PlannistPartsListViewModel : ViewModelBase
    {
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _requestStore;
        private string? _filterText;
        private List<ErrandPart> _parts = [];
        public PlannistPartsListViewModel(
            INotifier notifier,
            SelectedRequestStore selectedRequestStore,
            IProvider<ErrandPartStatus> errandPartStatusProvider,
            IDataStore<ErrandPart> errandPartStore,
            ICombinerManager<ErrandPart> errandPartCombiner)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            _errandPartCombiner = errandPartCombiner;
            SelectedRow = null;

            LoadData();

            ConfirmCommand = new PlannistConfirmCommand(_requestStore, notifier, (ErrandPartStatusProvider)errandPartStatusProvider);
            CancelCommand = new PlannistCancelCommand();
            AbortCommand = new PlannistAbortCommand(_requestStore, errandPartStatusProvider);
            DetailsCommand = new PlannistDetailsCommand();

            ConfirmCommand.RequestConfirmed += OnRequestChanged;
            AbortCommand.RequestAborted += OnRequestChanged;
        }
        public PlannistAbortCommand AbortCommand { get; }
        public ICommand CancelCommand { get; }
        public PlannistConfirmCommand ConfirmCommand { get; }
        public ICommand DetailsCommand { get; }
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
                else
                {
                    Parts.Filter = FilterParts;
                }
            }
        }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
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
            _parts = _errandPartStore.Data;

            Application.Current.Dispatcher.Invoke(Parts.Refresh);

            return Task.CompletedTask;
        }
        private void OnRequestChanged() => Parts.Refresh();
    }
}
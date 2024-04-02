using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
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
    internal sealed class OffersViewModel : ViewModelBase
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
                Parts.Filter = string.IsNullOrEmpty(value) ? null : FilterParts;
            }
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICommand OfferCommand { get; }
        public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public OffersViewModel(SelectedRequestStore selectedRequestStore, ErrandPartStore errandPartStore, NavigationService navigationService)
        {
            Task.Run(LoadData);

            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            SelectedRow = null;

            OfferCommand = new RelayCommand(o => { navigationService.NavigateTo<AddOfferViewModel>(); }, o => true);
            DetailsCommand = new PlannistDetailsCommand();
        }
        private void OnRequestChanged() => Parts.Refresh();
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_parts.Clear);

            await FillLists(_errandPartStore);

            Application.Current.Dispatcher.Invoke(Parts.Refresh);
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
                if (errandPart.LastStatusText == "OFERTOWANIE" && errandPart.LastStatus.Confirmed) _parts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
        private bool FilterParts(object obj)
        {
            if (string.IsNullOrEmpty(_filterText)) return true;
            if (obj is ErrandPart errandPart) return errandPart.SearchValue.Contains(_filterText, StringComparison.InvariantCultureIgnoreCase);
            return false;
        }
    }
}
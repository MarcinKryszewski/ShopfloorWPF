using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Plannist
{
    internal sealed class OffersViewModel : ViewModelBase
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
                Parts.Filter = string.IsNullOrEmpty(value) ? null : FilterParts;
            }
        }
        public ICollectionView Parts => CollectionViewSource.GetDefaultView(_parts);
        public ICommand OfferCommand { get; }
        public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public OffersViewModel(SelectedRequestStore selectedRequestStore, ErrandPartStore errandPartStore, NavigationService navigationService, ErrandPartCombiner errandPartCombiner)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            _errandPartCombiner = errandPartCombiner;
            SelectedRow = null;

            OfferCommand = new NavigationCommand<AddOfferViewModel>(navigationService).Navigate();
            //OfferCommand = new NavigateCommand<AddOfferViewModel>(navigationService).Navigate();
            DetailsCommand = new PlannistDetailsCommand();

            Task.Run(LoadData);
        }
        private void OnRequestChanged() => Parts.Refresh();
        private Task LoadData()
        {
            _errandPartCombiner.Combine().Wait();
            _parts = _errandPartStore.Data.Where(part => part.LastStatusText == ErrandPartStatus.Status[0] && part.LastStatus.Confirmed == true).ToList();

            Application.Current.Dispatcher.Invoke(Parts.Refresh);

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
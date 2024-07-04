using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Plannist
{
    internal sealed class OffersViewModel : ViewModelBase
    {
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _requestStore;
        private string? _filterText;
        private List<ErrandPart> _parts = [];
        public OffersViewModel(SelectedRequestStore selectedRequestStore, IDataStore<ErrandPart> errandPartStore, NavigationService navigationService, ICombinerManager<ErrandPart> errandPartCombiner)
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
        public ICommand DetailsCommand { get; }
        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                Parts.Filter = string.IsNullOrEmpty(value) ? null : FilterParts;
            }
        }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public ICommand OfferCommand { get; }
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
            _parts = _errandPartStore.Data.Where(part => part.LastStatusText == ErrandPartStatus.Status[0] && part.LastStatus.Confirmed).ToList();

            Application.Current.Dispatcher.Invoke(Parts.Refresh);

            return Task.CompletedTask;
        }
        private void OnRequestChanged() => Parts.Refresh();
    }
}
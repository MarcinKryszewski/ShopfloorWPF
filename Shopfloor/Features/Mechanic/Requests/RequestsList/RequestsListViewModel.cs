using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Requests
{
    internal sealed class RequestsListViewModel : ViewModelBase
    {
        private List<ErrandPart> _parts = [];
        private readonly SelectedRequestStore _requestStore;
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<ErrandPart> _errandPartStore;
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
        public ICommand EditCommand { get; }
        public ICommand DetailsCommand { get; }
        public Visibility HasAccess { get; } = Visibility.Collapsed;
        public RequestsListViewModel(ICurrentUserStore currentUserStore, SelectedRequestStore selectedRequestStore, ICombinerManager<ErrandPart> errandPartCombiner, IDataStore<ErrandPart> errandPartStore)
        {

            _requestStore = selectedRequestStore;
            _errandPartCombiner = errandPartCombiner;
            _errandPartStore = errandPartStore;
            SelectedRow = null;

            FillPartList();

            //EditCommand = new NavigateCommand<RequestsEditViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsEditViewModel>>());
            //DetailsCommand = new NavigateCommand<RequestsDetailsViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsDetailsViewModel>>());

            if (currentUserStore.User?.IsAuthorized(568) ?? false) HasAccess = Visibility.Visible;
        }
        private Task FillPartList()
        {
            _errandPartCombiner.CombineAll().Wait();
            _parts = _errandPartStore.Data;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Parts.Refresh();
            });

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
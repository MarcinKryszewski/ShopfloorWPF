﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Plannist
{
    internal sealed class DeploysViewModel : ViewModelBase
    {
        private readonly ICombinerManager<ErrandPart> _errandPartCombiner;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _requestStore;
        private string? _filterText;
        private List<ErrandPart> _parts = [];
        public DeploysViewModel(SelectedRequestStore selectedRequestStore, IDataStore<ErrandPart> errandPartStore, ICombinerManager<ErrandPart> errandPartCombiner)
        {
            _requestStore = selectedRequestStore;
            _errandPartStore = errandPartStore;
            _errandPartCombiner = errandPartCombiner;
            SelectedRow = null;

            //OfferCommand = new NavigateCommand<AddOfferViewModel>(_mainServices.GetRequiredService<NavigationService<AddOfferViewModel>>());
            //DetailsCommand = new PlannistDetailsCommand();
            Task.Run(LoadData);
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
        //public ICommand OfferCommand { get; }
        //public ICommand DetailsCommand { get; }
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
            _parts = _errandPartStore.Data.Where(part => part.LastStatusText == ErrandPartStatus.Status[6] && part.LastStatus.Confirmed).ToList();

            Application.Current.Dispatcher.Invoke(Parts.Refresh);

            return Task.CompletedTask;
        }
        private void OnRequestChanged() => Parts.Refresh();
    }
}
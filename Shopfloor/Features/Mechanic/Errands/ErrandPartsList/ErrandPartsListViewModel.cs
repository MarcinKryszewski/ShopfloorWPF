using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands.ErrandPartsList
{
    public class ErrandPartsListViewModel : ViewModelBase
    {
        private readonly List<Part> _parts = [];
        private readonly IServiceProvider _databaseServices;
        private readonly SelectedErrandStore _errandStore;
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                PartsAll.Filter = FilterParts;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        private bool FilterParts(object obj)
        {
            if (obj is Part part)
            {
                return part.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        public int PartsAmount => _errandStore.ErrandParts.Count;
        public ICollectionView PartsAll => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView PartsMachine => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView ErrandParts => CollectionViewSource.GetDefaultView(_errandStore.ErrandParts);
        public ICommand AddPartToListCommand { get; }
        public ICommand RemovePartFromListCommand { get; }
        public Part? SelectedPart { get; set; }
        public ErrandPartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _errandStore = mainServices.GetRequiredService<SelectedErrandStore>();
            AddPartToListCommand = new ErrandAddPartCommand(this, _errandStore);
            RemovePartFromListCommand = new ErrandRemovePartCommand(this, _errandStore);
            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke
            (() =>
            {
                _parts.Clear();
                _errandStore.ErrandParts.Clear();
            });

            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();

            List<Task> tasks = [];
            if (!partsStore.IsLoaded) tasks.Add(LoadParts(partsStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            tasks.Clear();

            tasks.Add(FillPartsList(partsStore));
            tasks.Add(FillErrandPartsList(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            Application.Current.Dispatcher.Invoke
            (() =>
            {
                PartsAll.Refresh();
                PartsMachine.Refresh();
                ErrandParts.Refresh();
            });
        }
        private Task LoadParts(PartsStore partsStore)
        {
            partsStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }
        private Task FillPartsList(PartsStore partsStore)
        {
            foreach (Part part in partsStore.Data)
            {
                _parts.Add(part);
            }
            return Task.CompletedTask;
        }
        private Task FillErrandPartsList(ErrandPartStore errandPartStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                if (errandPart.ErrandId == _errandStore.ErrandId) _errandStore.ErrandParts.Add(errandPart);
            }
            return Task.CompletedTask;
        }
    }
}
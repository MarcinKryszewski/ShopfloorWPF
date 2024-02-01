using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.ErrandPartsList
{
    public class ErrandPartsListViewModel : ViewModelBase
    {
        private List<Part> _parts = [];
        private List<ErrandPart> _errandParts = [];
        private readonly IServiceProvider _databaseServices;
        private readonly SelectedErrandStore _errandStore;
        public ICollectionView PartsAll => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView PartsMachine => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView ErrandParts => CollectionViewSource.GetDefaultView(_errandParts);
        public ErrandPartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _errandStore = mainServices.GetRequiredService<SelectedErrandStore>();
            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke
            (() =>
            {
                _parts.Clear();
                _errandParts.Clear();
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
                if (errandPart.ErrandId == _errandStore.ErrandId) _errandParts.Add(errandPart);
            }

            ErrandPart errandPart1 = new(2, 1)
            {
                Part = new Part(1, "Łożysko", null, null, null, null, null, null, null)
            };
            _errandParts.Add(errandPart1);

            return Task.CompletedTask;
        }
    }
}
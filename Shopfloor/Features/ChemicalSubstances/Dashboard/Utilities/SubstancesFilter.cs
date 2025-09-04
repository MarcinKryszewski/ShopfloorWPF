using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.Substances;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Shared;
using Shopfloor.Shared.HelperFunctions;

namespace Shopfloor.Features.ChemicalSubstances.Dashboard.Utilities
{
    internal class SubstancesFilter : ObservableObject
    {
        private static readonly object _syncLock = new();
        private readonly IDataRoot _data;
        private readonly List<Workshop> _workshops = [];
        private bool? _isFoodSafe = null;
        private string _substanceName = string.Empty;
        private string _workshop = string.Empty;
        public SubstancesFilter(IDataRoot data)
        {
            _data = data;
            Task.Run(LoadDataAsync);
        }
        public event EventHandler? FiltersChanged;
        public ICollectionView Workshops => CollectionViewSource.GetDefaultView(_workshops);
        public Workshop? SelectedWorkshop { get; set; }
        public bool? IsFoodSafe
        {
            get => _isFoodSafe;
            set
            {
                _isFoodSafe = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string SubstanceName
        {
            get => _substanceName;
            set
            {
                _substanceName = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Workshop
        {
            get => _workshop;
            set
            {
                _workshop = value;
                if (!CollectionHelper.IsInIEnumerable<Workshop>(_workshop, Workshops))
                {
                    SelectedWorkshop = null;
                    OnPropertyChanged(nameof(SelectedWorkshop));
                }
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        protected void OnFiltersChanged(EventArgs e) => FiltersChanged?.Invoke(this, e);
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadWorkshops());
            //TODO
            // loadProducers

            await Task.WhenAll(tasks);
        }
        private async Task LoadProducers()
        {

        }
        private async Task LoadWorkshops()
        {
            IEnumerable<Workshop> data = await _data.GetWorkshops();
            await BatchListUpdater.UpdateAsync(data, _workshops);
        }
    }
}
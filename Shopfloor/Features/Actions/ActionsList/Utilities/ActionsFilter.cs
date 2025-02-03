using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Shared;
using Shopfloor.Shared.HelperFunctions;

namespace Shopfloor.Features.Actions.ActionsList.Utilities
{
    internal class ActionsFilter : ObservableObject
    {
        private static readonly object _syncLock = new();
        private readonly DataRoot _data;
        private readonly List<Line> _lines = [];
        private readonly List<Machine> _machines = [];
        private readonly List<ActivityType> _types = [];
        private readonly List<Workshop> _workshops = [];
        private bool? _hasInstruction = null;
        private bool _isJog = false;
        private bool _isLoto = false;
        private bool _isProduction = false;
        private string _line = string.Empty;
        private string _machine = string.Empty;
        private string _type = string.Empty;
        private string _workshop = string.Empty;
        public ActionsFilter(DataRoot data)
        {
            _data = data;
            Machines = new ListCollectionView(_machines)
            {
                Filter = Filter,
            };

            Task.Run(LoadDataAsync);
        }
        public event EventHandler? FiltersChanged;
        public bool? HasInstruction
        {
            get => _hasInstruction;
            set
            {
                _hasInstruction = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public bool IsJog
        {
            get => _isJog;
            set
            {
                _isJog = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public bool IsLoto
        {
            get => _isLoto;
            set
            {
                _isLoto = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public bool IsProduction
        {
            get => _isProduction;
            set
            {
                _isProduction = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Line
        {
            get => _line;
            set
            {
                string machineName = Machine;
                _line = value;
                if (!CollectionHelper.IsInIEnumerable<Line>(_line, Lines))
                {
                    SelectedLine = null;
                    OnPropertyChanged(nameof(SelectedLine));
                }
                Machines.Refresh();
                if (!CollectionHelper.IsInIEnumerable<Machine>(machineName, Machines))
                {
                    Machine = string.Empty;
                    OnPropertyChanged(nameof(Machine));
                }
                OnPropertyChanged(nameof(Line));
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public string Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                if (!CollectionHelper.IsInIEnumerable<Machine>(_machine, Machines))
                {
                    SelectedMachine = null;
                    OnPropertyChanged(nameof(SelectedMachine));
                }
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public ICollectionView Machines { get; init; }
        public Line? SelectedLine { get; set; }
        public Machine? SelectedMachine { get; set; }
        public ActivityType? SelectedType { get; set; }
        public Workshop? SelectedWorkshop { get; set; }
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                if (!CollectionHelper.IsInIEnumerable<ActivityType>(_type, Types))
                {
                    SelectedType = null;
                    OnPropertyChanged(nameof(SelectedType));
                }
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public ICollectionView Types => CollectionViewSource.GetDefaultView(_types);
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
        public ICollectionView Workshops => CollectionViewSource.GetDefaultView(_workshops);
        protected void OnFiltersChanged(EventArgs e) => FiltersChanged?.Invoke(this, e);
        private bool Filter(object obj)
        {
            if (obj is Machine machine)
            {
                bool line =
                    string.IsNullOrEmpty(Line) ||
                    machine.Line!.Name.Contains(Line, StringComparison.InvariantCultureIgnoreCase);

                return line;
            }
            return false;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadWorkshops());
            tasks.Add(LoadLines());
            tasks.Add(LoadMachines());
            tasks.Add(LoadTypes());

            await Task.WhenAll(tasks);
        }
        private async Task LoadLines()
        {
            IEnumerable<Line> data = await _data.GetLine();
            await BatchListUpdater.UpdateAsync(data, _lines);
        }
        private async Task LoadMachines()
        {
            IEnumerable<Machine> data = await _data.GetMachine();
            await BatchListUpdater.UpdateAsync(data, _machines);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                lock (_syncLock)
                {
                    OnPropertyChanged(nameof(Machines));
                    Machines.Refresh();
                }
            });
        }
        private async Task LoadTypes()
        {
            IEnumerable<ActivityType> data = await _data.GetActivityType();
            await BatchListUpdater.UpdateAsync(data, _types);
        }
        private async Task LoadWorkshops()
        {
            IEnumerable<Workshop> data = await _data.GetWorkshop();
            await BatchListUpdater.UpdateAsync(data, _workshops);
        }
    }
}
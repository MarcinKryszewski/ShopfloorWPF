using Shopfloor.Features.Plannist.PlannistDashboard.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Features.Plannist.PlannistDashboard
{
    internal sealed class PaginatedFilterableList : INotifyPropertyChanged
    {
        private readonly List<ISearchableModel> _dataSource = [];
        private readonly int _pageSize = 25;
        private int _currentPage = 1;
        private List<ISearchableModel> _dataDisplay = [];
        private IEnumerable<ISearchableModel> _dataFiltered = [];
        private string _filterText = string.Empty;
        public PaginatedFilterableList(int pageSize, List<ISearchableModel> dataSource)
        {
            _pageSize = pageSize;
            _dataSource = dataSource;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public string CurrentPageText => $"{CurrentPage} z {MaxPage()}";
        public List<ISearchableModel> DataDisplay => _dataDisplay;
        public Task ChangePage()
        {
            if (_currentPage > MaxPage()) _currentPage = MaxPage();
            int indexStart = (_currentPage - 1) * _pageSize;

            _dataDisplay.Clear();
            List<ISearchableModel> models = new(_dataFiltered);
            _dataDisplay = models.GetRange(indexStart, Math.Min(_pageSize, models.Count - indexStart));

            OnPropertyChanged(nameof(DataDisplay));
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CurrentPageText));

            return Task.CompletedTask;
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void PageNext()
        {
            int nextLBound = _currentPage * _pageSize + 1;
            if (nextLBound <= _dataSource.Count)
            {
                CurrentPage++;
                ChangePage();
            }
        }
        public void PagePrev()
        {
            if (_currentPage > 1)
            {
                CurrentPage--;
                ChangePage();
            }
        }
        private async Task FilterList()
        {
            if (string.IsNullOrEmpty(_filterText))
            {
                _dataFiltered = _dataSource;
                await ChangePage();
                return;
            }

            List<ISearchableModel> models = [];
            string filterTextNormalized = RemovePolishCharacters.Remove(_filterText.ToLower());

            // HashSet?
            foreach (ISearchableModel model in _dataSource)
            {
                if (model.SearchValue.Contains(filterTextNormalized)) models.Add(model);
            }
            _dataFiltered = models;
            await ChangePage();
        }
        private int MaxPage()
        {
            int pagesAmount = (int)Math.Ceiling((double)_dataFiltered.Count() / _pageSize);
            return Math.Max(pagesAmount, 1);
        }
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                _ = FilterList();
            }
        }
    }
    internal sealed class PlannistDashboardMainViewModel : ViewModelBase
    {
        private readonly List<Part> _dataSource = [];
        private readonly int _pageSize = 25;
        private int _currentPage = 1;
        private List<Part> _dataDisplay = [];
        private IEnumerable<Part> _dataFiltered = [];
        private string _filterText = string.Empty;
        //public PaginatedFilterableList DisplayList { get; }
        public PlannistDashboardMainViewModel(IServiceProvider mainServices)
        {
            //DisplayList = new(25, _dataSource);
            LoadExcel = new LoadExcelDataCommand(this);
            NextPage = new NextPageCommand(this);
            PrevPage = new PreviousPageCommand(this);
        }
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public string CurrentPageText => $"{CurrentPage} z {MaxPage()}";
        public List<Part> DataDisplay => _dataDisplay;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                _ = FilterList();
            }
        }
        public ICommand LoadExcel { get; }
        public ICommand NextPage { get; }
        public ICommand PrevPage { get; }
        public Task ChangePage()
        {
            if (_currentPage > MaxPage()) _currentPage = MaxPage();
            int indexStart = (_currentPage - 1) * _pageSize;

            _dataDisplay.Clear();
            List<Part> parts = new(_dataFiltered);
            _dataDisplay = parts.GetRange(indexStart, Math.Min(_pageSize, parts.Count - indexStart));

            OnPropertyChanged(nameof(DataDisplay));
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CurrentPageText));

            return Task.CompletedTask;
        }
        public async Task LoadData(List<Part> parts)
        {
            _dataSource.Clear();
            foreach (Part part in parts)
            {
                _dataSource.Add(part);
            }
            _dataFiltered = _dataSource;
            await Application.Current.Dispatcher.InvokeAsync(ChangePage);
            OnPropertyChanged(nameof(CurrentPageText));
        }
        public void PageNext()
        {
            int nextLBound = _currentPage * _pageSize + 1;
            if (nextLBound <= _dataSource.Count)
            {
                CurrentPage++;
                ChangePage();
            }
        }
        public void PagePrev()
        {
            if (_currentPage > 1)
            {
                CurrentPage--;
                ChangePage();
            }
        }
        private async Task FilterList()
        {
            if (string.IsNullOrEmpty(_filterText))
            {
                _dataFiltered = _dataSource;
                await ChangePage();
                return;
            }

            List<Part> parts = [];
            string filterTextNormalized = RemovePolishCharacters.Remove(_filterText.ToLower());

            // HashSet?
            foreach (Part part in _dataSource)
            {
                if (part.SearchValue.Contains(filterTextNormalized)) parts.Add(part);
            }
            _dataFiltered = parts;
            await ChangePage();
        }
        private int MaxPage()
        {
            int pagesAmount = (int)Math.Ceiling((double)_dataFiltered.Count() / _pageSize);
            return Math.Max(pagesAmount, 1);
        }
    }
}
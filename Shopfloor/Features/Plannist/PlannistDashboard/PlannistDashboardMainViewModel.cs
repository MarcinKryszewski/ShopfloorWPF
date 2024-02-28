using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Input;

namespace Shopfloor.Features.Plannist.PlannistDashboard
{
    internal sealed class PaginatedFilterableList : INotifyPropertyChanged
    {
        private readonly IEnumerable<ISearchableModel> _dataSource = [];
        private readonly int _pageSize = 25;
        private int _currentPage = 1;
        private List<ISearchableModel> _dataDisplay = [];
        private List<ISearchableModel> _dataFiltered = [];
        private string _filterText = string.Empty;
        public PaginatedFilterableList(int pageSize, IEnumerable<ISearchableModel> dataSource)
        {
            _pageSize = pageSize;
            _dataSource = dataSource;
            _dataFiltered = new(dataSource);
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
        public async Task ReloadSourceData()
        {
            _filterText = string.Empty;
            await FilterList();
        }
        public Task ChangePage()
        {
            int maxPage = MaxPage();
            if (_currentPage > maxPage) _currentPage = maxPage;
            int indexStart = (_currentPage - 1) * _pageSize;

            _dataDisplay = _dataFiltered.GetRange(indexStart, Math.Min(_pageSize, _dataFiltered.Count - indexStart));

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
            if (nextLBound <= _dataFiltered.Count)
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
                _dataFiltered = new(_dataSource);
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
        public List<Part> DataSource => _dataSource;
        public PaginatedFilterableList DisplayList { get; }
        public ICommand NextPage { get; }
        public ICommand PrevPage { get; }
        public ICommand UpdateValues { get; }
        public PlannistDashboardMainViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            DisplayList = new(25, _dataSource);
            LoadExcel = new LoadExcelDataCommand(this);
            NextPage = new NextPageCommand(DisplayList);
            PrevPage = new PreviousPageCommand(DisplayList);
            UpdateValues = new UpdateDataCommand(this, databaseServices.GetRequiredService<PartProvider>());
        }
        public ICommand LoadExcel { get; }
        public async Task LoadData(List<Part> parts)
        {
            _dataSource.Clear();
            foreach (Part part in parts)
            {
                _dataSource.Add(part);
            }
            await DisplayList.ReloadSourceData();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Utilities.CustomList
{
    internal sealed class SearchableModelList : INotifyPropertyChanged
    {
        private readonly IEnumerable<ISearchableModel> _dataSource = [];
        private readonly int _pageSize;
        private int _currentPage = 1;
        private List<ISearchableModel> _dataDisplay = [];
        private List<ISearchableModel> _dataFiltered = [];
        private string _filterText = string.Empty;
        public SearchableModelList(IEnumerable<ISearchableModel> dataSource, int pageSize = 25)
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
        public Task PageChanged()
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
                PageChanged();
            }
        }
        public void PagePrev()
        {
            if (_currentPage > 1)
            {
                CurrentPage--;
                PageChanged();
            }
        }
        public void PageFirst()
        {
            CurrentPage = 1;
            PageChanged();
        }
        public void PageLast()
        {
            CurrentPage = MaxPage();
            PageChanged();
        }
        public void PageSet(int pageNumber)
        {
            if (pageNumber < 1) return;
            CurrentPage = pageNumber > MaxPage() ? MaxPage() : pageNumber;
            PageChanged();
        }
        private async Task FilterList()
        {
            if (string.IsNullOrEmpty(_filterText))
            {
                _dataFiltered = new(_dataSource);
                await PageChanged();
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
            await PageChanged();
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
}
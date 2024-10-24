using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Utilities.CustomList
{
    internal sealed partial class SearchableModelList
    {
        private readonly int _pageSize;
        private int _currentPage = 1;
        private List<ISearchableModel> _dataDisplay = [];
        private List<ISearchableModel> _dataFiltered;
        private string _filterText = string.Empty;
        public SearchableModelList(IEnumerable<ISearchableModel> dataSource, int pageSize = 25)
        {
            _pageSize = pageSize;
            Data = dataSource;
            _dataFiltered = new(dataSource);
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
        public IEnumerable<ISearchableModel> Data { get; set; }
        public List<ISearchableModel> DataDisplay => _dataDisplay;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                FilterList().Wait();
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
        public void PageNext()
        {
            int nextLBound = (_currentPage * _pageSize) + 1;
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
        public void PageSet(int pageNumber)
        {
            if (pageNumber < 1)
            {
                return;
            }

            CurrentPage = pageNumber > MaxPage() ? MaxPage() : pageNumber;
            PageChanged();
        }
        public async Task ReloadSourceData()
        {
            _filterText = string.Empty;
            await FilterList();
        }
        private async Task FilterList()
        {
            if (string.IsNullOrEmpty(_filterText))
            {
                _dataFiltered = new(Data);
                await PageChanged();
                return;
            }

            List<ISearchableModel> models = [];
            string filterTextNormalized = RemovePolishCharacters.Remove(_filterText.ToLower());
            models.AddRange(Data.Where(model => model.SearchValue.Contains(filterTextNormalized)));
            _dataFiltered = models;
            await PageChanged();
        }
        private int MaxPage()
        {
            int pagesAmount = (int)Math.Ceiling((double)_dataFiltered.Count / _pageSize);
            return Math.Max(pagesAmount, 1);
        }
        private Task PageChanged()
        {
            int maxPage = MaxPage();
            if (_currentPage > maxPage)
            {
                _currentPage = maxPage;
            }

            int indexStart = (_currentPage - 1) * _pageSize;

            _dataDisplay = _dataFiltered.GetRange(indexStart, Math.Min(_pageSize, _dataFiltered.Count - indexStart));

            OnPropertyChanged(nameof(DataDisplay));
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CurrentPageText));

            return Task.CompletedTask;
        }
    }
    internal sealed partial class SearchableModelList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
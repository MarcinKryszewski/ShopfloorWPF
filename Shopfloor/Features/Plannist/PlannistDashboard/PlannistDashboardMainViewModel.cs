using Shopfloor.Features.Plannist.PlannistDashboard.Commands;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace Shopfloor.Features.Plannist.PlannistDashboard
{
    internal sealed class PlannistDashboardMainViewModel : ViewModelBase
    {
        private readonly List<Part> _data = [];
        public PagingCollectionView Data { get; }
        public ICommand LoadExcel { get; }
        public ICommand PreviousPage { get; }
        public ICommand NextPage { get; }
        public PlannistDashboardMainViewModel(IServiceProvider mainServices)
        {
            LoadExcel = new LoadExcelDataCommand(this);
            PreviousPage = new PreviousPageCommand(this);
            NextPage = new NextPageCommand(this);
            Data = new(_data, 20)
            {
                Filter = FilterData
            };
        }
        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = RemovePolishCharacters.Remove(value.ToLower());
                Data.Refresh();
                OnPropertyChanged(nameof(FilterText));
            }
        }
        private bool FilterData(object obj)
        {
            if (string.IsNullOrEmpty(_filterText)) return true;
            if (obj is Part Part)
            {
                return Part.SearchValue.Contains(_filterText);
            }
            return false;
        }
        public async Task LoadData(List<Part> parts)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
            _data.Clear();
            foreach (Part part in parts)
            {
                _data.Add(part);
            }
            await Application.Current.Dispatcher.InvokeAsync(Data.Refresh);
        }
        public void ClearData()
        {
            _data.Clear();
            Data.Refresh();
        }
    }

    public class PagingCollectionView : CollectionView
    {
        private readonly int _itemsPerPage;

        private int _currentPage = 1;

        public PagingCollectionView(IEnumerable innerList, int itemsPerPage)
            : base(innerList)
        {
            _itemsPerPage = itemsPerPage;
        }

        public int FilteredCount
        {
            get { return FilteredCollection.Count(); }
        }

        private IEnumerable<object> FilteredCollection
        {
            get
            {
                if (Filter is null) return SourceCollection.OfType<object>();
                return SourceCollection.OfType<object>().Where(o => Filter(o));
            }
        }

        public override int Count
        {
            get
            {
                if (FilteredCount == 0) return 0;
                if (_currentPage < PageCount) // page 1..n-1
                {
                    return _itemsPerPage;
                }
                else // page n
                {
                    var itemsLeft = FilteredCount % _itemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return _itemsPerPage; // exactly itemsPerPage left
                    }
                    else
                    {
                        // return the remaining items
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                if (_currentPage > PageCount) CurrentPage = PageCount;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        public int ItemsPerPage { get { return _itemsPerPage; } }

        public int PageCount
        {
            get
            {
                return (FilteredCount + _itemsPerPage - 1)
                    / _itemsPerPage;
            }
        }

        private int EndIndex
        {
            get
            {
                var end = _currentPage * _itemsPerPage - 1;
                return (end > FilteredCount) ? FilteredCount : end;
            }
        }

        private int StartIndex
        {
            get { return (_currentPage - 1) * _itemsPerPage; }
        }

        public override object GetItemAt(int index)
        {
            if (FilteredCount == 0) return null;
            var offset = index % _itemsPerPage;
            return FilteredCollection.ElementAt(StartIndex + offset);
        }

        public void MoveToNextPage()
        {
            if (_currentPage < PageCount)
            {
                CurrentPage += 1;
            }
            Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (_currentPage > 1)
            {
                CurrentPage -= 1;
            }
            Refresh();
        }
    }
}
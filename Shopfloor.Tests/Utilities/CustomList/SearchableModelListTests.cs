using FluentAssertions;
using NSubstitute;
using Shopfloor.Interfaces;
using Shopfloor.Utilities.CustomList;

namespace Shopfloor.Tests.Utilities.CustomList
{
    public class SearchableModelListTests
    {
        private IEnumerable<ISearchableModel> GetData()
        {
            ISearchableModel model1 = Substitute.For<ISearchableModel>();
            model1.Id.Returns(1);
            model1.SearchValue.Returns("");

            ISearchableModel model2 = Substitute.For<ISearchableModel>();
            model2.Id.Returns(2);
            model2.SearchValue.Returns("");

            ISearchableModel model3 = Substitute.For<ISearchableModel>();
            model3.Id.Returns(3);
            model3.SearchValue.Returns("");

            ISearchableModel model4 = Substitute.For<ISearchableModel>();
            model4.Id.Returns(4);
            model4.SearchValue.Returns("");

            return [model1, model2, model3, model4];
        }
        [Fact]
        public void CurrentPageText_ShouldReturnCorrectString()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, 1);

            // Act
            string currentPageText = sut.CurrentPageText;
            string correctString = $"1 z {data.Count()}";
            bool result = correctString.Equals(currentPageText);

            // Assert
            result.Should().BeTrue();
            currentPageText.Should().NotBeNullOrEmpty();
            currentPageText.Length.Should().BeGreaterThan(0);
        }
        [Fact]
        public void CurrentPage_ShouldBeSetToFirst_AfterObjectCreate()
        {
            // Arrange
            SearchableModelList sut = new(GetData(), 1);

            // Act
            int result = sut.CurrentPage;

            // Assert
            result.Should().Be(1);
            result.Should().BePositive();
        }
        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(4)]
        public void PageNext_ShouldChangeCurrentPage(int? value)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = value is null ? new(data) : new(data, (int)value);
            int startingPageNumber = sut.CurrentPage;
            int maxPage = (int)Math.Ceiling((double)data.Count() / (value ?? 25));
            // Act
            sut.PageNext();
            int result = sut.CurrentPage;
            // Assert
            result.Should().BeGreaterThanOrEqualTo(startingPageNumber);
            result.Should().BeLessThanOrEqualTo(maxPage);
        }
        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(4)]
        public void PagePrev_ShouldChangeCurrentPage(int? value)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = value is null ? new(data) : new(data, (int)value);
            int startingPageNumber = sut.CurrentPage;
            // Act
            sut.PagePrev();
            int result = sut.CurrentPage;
            // Assert
            result.Should().BeLessThanOrEqualTo(startingPageNumber);
            result.Should().BeGreaterThanOrEqualTo(1);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void PageFirst_ShouldChangeCurrentPageToFirst(int? value)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = value is null ? new(data) : new(data, (int)value);
            // Act
            sut.PageNext();
            sut.PageNext();
            sut.PageFirst();
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(1);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void PageLast_ShouldChangeCurrentPageToLast(int? value)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = value is null ? new(data) : new(data, (int)value);
            int maxPage = (int)Math.Ceiling((double)data.Count() / (value ?? 25));
            // Act
            sut.PageLast();
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(maxPage);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void PageSet_WithValidPageNumber_ShouldSetCurrentPage(int pageNumber)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, 1);
            // Act
            sut.PageSet(pageNumber);
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(pageNumber);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void PageSet_WithPageNumberLessThanOne_ShouldNotChangeCurrentPage(int pageNumber)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, 1);
            int startingPageNumber = sut.CurrentPage;
            // Act
            sut.PageSet(pageNumber);
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(startingPageNumber);
        }
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public void PageSet_WithPageNumberGreaterThanMaxPage_ShouldSetCurrentPageToMaxPage(int pageNumber)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, 1);
            int maxPage = 4;
            // Act
            sut.PageSet(pageNumber);
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(maxPage);
        }
        /*[Fact]
        public void CurrentPageText_ShouldUpdateWithCurrentPageChange()
        {
            // Arrange
            var searchableModelList = new SearchableModelList();
            searchableModelList.SetDataFiltered(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            searchableModelList.PageSize = 3;

            // Act
            searchableModelList.CurrentPage = 3;
            string currentPageTextAfterChange = searchableModelList.CurrentPageText;

            // Assert
            Assert.Equal("3 z 4", currentPageTextAfterChange); // Assuming there are 4 total pages
        }*/
    }
}
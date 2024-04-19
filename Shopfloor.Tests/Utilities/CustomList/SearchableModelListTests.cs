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
            model1.SearchValue.Returns("test1");

            ISearchableModel model2 = Substitute.For<ISearchableModel>();
            model2.Id.Returns(2);
            model2.SearchValue.Returns("test11");

            ISearchableModel model3 = Substitute.For<ISearchableModel>();
            model3.Id.Returns(3);
            model3.SearchValue.Returns("test2");

            ISearchableModel model4 = Substitute.For<ISearchableModel>();
            model4.Id.Returns(4);
            model4.SearchValue.Returns("test22");

            return [model1, model2, model3, model4];
        }
        [Fact]
        public void CurrentPageText_ShouldReturnCorrectString()
        {
            // Arrange
            int pageSize = 1;
            IEnumerable<ISearchableModel> data = GetData();
            int dataSampleSize = data.Count();
            SearchableModelList sut = new(data, pageSize);
            // Act
            string currentPageText = sut.CurrentPageText;
            string correctString = $"1 z {dataSampleSize}";
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
            int pageSize = 1;
            SearchableModelList sut = new(GetData(), pageSize);
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
        [Fact]
        public void PageNext_PageShouldNotChange_WhenCurrentPageIsBiggerThanMax()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            int pageSize = 1;
            int currentPage;
            SearchableModelList sut = new(data, pageSize);
            int maxPage = (int)Math.Ceiling((double)data.Count() / pageSize);
            // Act
            sut.PageSet(maxPage);
            currentPage = sut.CurrentPage;
            sut.PageNext();
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(maxPage);
            result.Should().Be(currentPage);
        }
        [Fact]
        public void PagePrev_ShouldChangeCurrentPage()
        {
            // Arrange
            int pageSize = 1;
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, pageSize);
            int startingPageNumber = sut.CurrentPage;
            // Act
            sut.PageSet(2);
            sut.PagePrev();
            int result = sut.CurrentPage;
            // Assert
            result.Should().BeLessThanOrEqualTo(startingPageNumber);
            result.Should().BeGreaterThanOrEqualTo(1);
        }
        [Fact]
        public void PagePrev_PageShouldNotChange_WhenCurrentPageIsFirst()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            int pageSize = 1;
            int currentPage;
            SearchableModelList sut = new(data, pageSize);
            // Act
            sut.PageSet(1);
            currentPage = sut.CurrentPage;
            sut.PagePrev();
            int result = sut.CurrentPage;
            // Assert
            result.Should().Be(1);
            result.Should().Be(currentPage);
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
        [Fact]
        public void CurrentPageText_ShouldUpdateWithCurrentPageChange()
        {
            // Arrange
            int pageSize = 1;
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, pageSize);
            int maxPage = data.Count();
            // Act
            string currentPageText = sut.CurrentPageText;
            sut.PageNext();
            string result = sut.CurrentPageText;
            // Assert
            result.Should().NotBe(currentPageText);
            result.Should().Be($"2 z {maxPage}");
        }
        [Fact]
        public void FilterText_ShouldFilterData_WhenSet()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, data.Count());
            string filterText = "test1";
            int dataSampleSize = data.Count();
            // Act
            sut.FilterText = filterText;
            int result = sut.DataDisplay.Count;
            // Assert
            result.Should().Be(2);
            result.Should().NotBe(dataSampleSize);
        }
        [Fact]
        public void FilterText_ShouldReturnWholeSetData_WhenSetToEmpty()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, data.Count());
            string filterText = "test1";
            int dataSampleSize = data.Count();
            // Act
            sut.FilterText = filterText;
            int filteredDataAmount = sut.DataDisplay.Count;
            sut.FilterText = "";
            int result = sut.DataDisplay.Count;
            // Assert
            result.Should().Be(dataSampleSize);
            result.Should().NotBe(filteredDataAmount);
        }
        [Fact]
        public async Task ReloadSourceData_ShouldReturnWholeSetData()
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList sut = new(data, data.Count());
            string filterText = "test1";
            int dataSampleSize = data.Count();
            // Act
            sut.FilterText = filterText;
            int filteredDataAmount = sut.DataDisplay.Count;
            await sut.ReloadSourceData();
            int result = sut.DataDisplay.Count;
            // Assert
            result.Should().Be(dataSampleSize);
            result.Should().NotBe(filteredDataAmount);
        }
    }
}
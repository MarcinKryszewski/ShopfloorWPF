using Shopfloor.Interfaces;
using Shopfloor.Utilities.CustomList;
using Shopfloor.Utilities.CustomList.CustomListCommands;

namespace Shopfloor.Tests.Utilities.CustomList.CustomListCommands
{
    public class NextPageCommandTests
    {
        private static IEnumerable<ISearchableModel> GetData()
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
        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(4)]
        public void PageNext_ShouldChangeCurrentPage(int? value)
        {
            // Arrange
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList list = value is null ? new(data) : new(data, (int)value);
            int startingPageNumber = list.CurrentPage;
            int maxPage = (int)Math.Ceiling((double)data.Count() / (value ?? 25));
            NextPageCommand sut = new(list);
            // Act
            sut.Execute("");
            int result = list.CurrentPage;
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
            SearchableModelList list = new(data, pageSize);
            int maxPage = (int)Math.Ceiling((double)data.Count() / pageSize);
            NextPageCommand sut = new(list);
            // Act
            list.PageSet(maxPage);
            currentPage = list.CurrentPage;
            sut.Execute("");
            int result = list.CurrentPage;
            // Assert
            result.Should().Be(maxPage);
            result.Should().Be(currentPage);
        }
    }
}

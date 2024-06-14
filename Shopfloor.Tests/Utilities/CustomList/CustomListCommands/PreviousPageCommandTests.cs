using Shopfloor.Interfaces;
using Shopfloor.Utilities.CustomList;
using Shopfloor.Utilities.CustomList.CustomListCommands;

namespace Shopfloor.Tests.Utilities.CustomList.CustomListCommands
{
    public class PreviousPageCommandTests
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
        [Fact]
        public void PagePrev_ShouldChangeCurrentPage()
        {
            // Arrange
            int pageSize = 1;
            IEnumerable<ISearchableModel> data = GetData();
            SearchableModelList list = new(data, pageSize);
            int startingPageNumber = list.CurrentPage;
            PreviousPageCommand sut = new(list);
            // Act
            list.PageSet(2);
            sut.Execute("");
            int result = list.CurrentPage;
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
            SearchableModelList list = new(data, pageSize);
            PreviousPageCommand sut = new(list);
            // Act
            list.PageSet(1);
            currentPage = list.CurrentPage;
            sut.Execute("");
            int result = list.CurrentPage;
            // Assert
            result.Should().Be(1);
            result.Should().Be(currentPage);
        }
    }
}

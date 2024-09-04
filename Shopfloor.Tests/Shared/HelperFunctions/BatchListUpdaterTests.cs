using System.ComponentModel;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Tests.UtilityClasses;

namespace Shopfloor.Tests.Shared.HelperFunctions
{
    public class BatchListUpdaterTests
    {
        private readonly FakeDispatcherWrapper _fakeDispatcher = new();

        [Fact]
        public async Task UpdateAsync_ShouldAddItemsToPrivateListInBatches()
        {
            // Arrange
            List<int> data = Enumerable.Range(1, 25).ToList();
            List<int> privateList = [];
            ICollectionView mockPublicList = Substitute.For<ICollectionView>();
            int batchSize = 10;

            // Act
            await BatchListUpdater.UpdateAsync(data, privateList, mockPublicList, _fakeDispatcher, batchSize);

            // Assert
            privateList.Should().HaveCount(25);
            privateList.Should().BeEquivalentTo(data);
            mockPublicList.Received(3).Refresh();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5000)]
        public async Task UpdateAsync_ShouldUseDefaultBatchSize_WhenBatchSizeIsLessThanMinimum(int invalidBatchSize)
        {
            // Arrange
            List<int> data = Enumerable.Range(1, 20).ToList();
            List<int> privateList = [];
            ICollectionView mockPublicList = Substitute.For<ICollectionView>();

            // Act
            await BatchListUpdater.UpdateAsync(data, privateList, mockPublicList, _fakeDispatcher, invalidBatchSize);

            // Assert
            privateList.Should().HaveCount(20);
            privateList.Should().BeEquivalentTo(data);
            mockPublicList.Received(2).Refresh();
        }

        [Fact]
        public async Task UpdateAsync_ShouldRefreshPublicListAfterEachBatch()
        {
            // Arrange
            List<int> data = Enumerable.Range(1, 15).ToList();
            List<int> privateList = [];
            ICollectionView mockPublicList = Substitute.For<ICollectionView>();
            int batchSize = 5;

            // Act
            await BatchListUpdater.UpdateAsync(data, privateList, mockPublicList, _fakeDispatcher, batchSize);

            // Assert
            privateList.Should().HaveCount(15);
            mockPublicList.Received(3).Refresh();
        }

        [Fact]
        public async Task UpdateAsync_ShouldHandleEmptyDataGracefully()
        {
            // Arrange
            List<int> data = [];
            List<int> privateList = [];
            ICollectionView mockPublicList = Substitute.For<ICollectionView>();

            // Act
            await BatchListUpdater.UpdateAsync(data, privateList, mockPublicList, _fakeDispatcher);

            // Assert
            privateList.Should().BeEmpty();
            mockPublicList.DidNotReceive().Refresh();
        }

        [Fact]
        public async Task UpdateAsync_ShouldHandleSingleItemData()
        {
            // Arrange
            List<int> data = [1];
            List<int> privateList = [];
            ICollectionView mockPublicList = Substitute.For<ICollectionView>();
            int batchSize = 5;

            // Act
            await BatchListUpdater.UpdateAsync(data, privateList, mockPublicList, _fakeDispatcher, batchSize);

            // Assert
            privateList.Should().HaveCount(1);
            privateList.Should().Contain(1);
            mockPublicList.Received(1).Refresh();
        }
    }
}
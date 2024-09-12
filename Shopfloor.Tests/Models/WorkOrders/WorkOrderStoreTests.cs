using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Tests.Models.WorkOrders
{
    public class WorkOrderStoreTests
    {
        private readonly WorkOrderStore _workOrderStore;
        private readonly IRepository<WorkOrderModel> _repositoryMock;

        public WorkOrderStoreTests()
        {
            _repositoryMock = Substitute.For<IRepository<WorkOrderModel>>();
            _workOrderStore = new WorkOrderStore(_repositoryMock);
        }

        [Fact]
        public async Task GetDataAsync_ShouldLoadDataFromRepository_IfNotAlreadyLoaded()
        {
            // Arrange
            List<WorkOrderModel>? expectedData = [
                new() { Id = 1, Description = "Test Work Order 1", LineId = 1, CreateDate = DateTime.Now },
                new() { Id = 2, Description = "Test Work Order 2", LineId = 2, CreateDate = DateTime.Now }
            ];

            _repositoryMock.GetData().Returns(expectedData);

            // Act
            List<WorkOrderModel>? result = await _workOrderStore.GetDataAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedData);
            await _repositoryMock.Received(1).GetData();
        }

        [Fact]
        public async Task GetDataAsync_ShouldNotReloadData_IfAlreadyLoaded()
        {
            // Arrange
            List<WorkOrderModel>? firstLoadData = [
                new() { Id = 1, Description = "Test Work Order 1", LineId = 1, CreateDate = DateTime.Now }
            ];

            _repositoryMock.GetData().Returns(firstLoadData);

            // Act
            List<WorkOrderModel>? firstCall = await _workOrderStore.GetDataAsync();
            List<WorkOrderModel>? secondCall = await _workOrderStore.GetDataAsync();

            // Assert
            firstCall.Should().BeEquivalentTo(secondCall);
            await _repositoryMock.Received(1).GetData();
        }

        [Fact]
        public async Task ReloadData_ShouldOverwriteExistingData()
        {
            // Arrange
            List<WorkOrderModel>? initialData = [
                new() { Id = 1, Description = "Initial Work Order", LineId = 1, CreateDate = DateTime.Now }
            ];
            _repositoryMock.GetData().Returns(initialData);

            await _workOrderStore.GetDataAsync();

            // Act
            await _workOrderStore.ReloadData();
            List<WorkOrderModel>? result = await _workOrderStore.GetDataAsync();

            // Assert
            result.Should().HaveCount(20);
            result.Should().Contain(x => x.Description == "Montaż systemu klimatyzacji");
            await _repositoryMock.Received(1).GetData();
        }
    }
}
using Shopfloor.Models.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Tests.UnitOfWorks
{
    public class WorkOrdersListRootTests
    {
        private readonly WorkOrdersListRoot _workOrdersListRoot;
        private readonly IStore<WorkOrderModel> _workOrderStoreMock;
        private readonly IStore<LineModel> _lineStoreMock;

        public WorkOrdersListRootTests()
        {
            _workOrderStoreMock = Substitute.For<IStore<WorkOrderModel>>();
            _lineStoreMock = Substitute.For<IStore<LineModel>>();
            _workOrdersListRoot = new WorkOrdersListRoot(_workOrderStoreMock, _lineStoreMock);
        }

        [Fact]
        public async Task GetWorkOrders_ShouldReturnWorkOrdersFromStore()
        {
            // Arrange
            List<WorkOrderModel>? workOrders = [
                new WorkOrderModel { Id = 1, LineId = 1, Description = "Test WorkOrder 1", CreateDate = DateTime.MinValue },
                new WorkOrderModel { Id = 2, LineId = 2, Description = "Test WorkOrder 2", CreateDate = DateTime.MinValue }
            ];
            _workOrderStoreMock.GetDataAsync().Returns(workOrders);

            // Act
            IEnumerable<WorkOrderModel>? result = await _workOrdersListRoot.GetWorkOrders();

            // Assert
            result.Should().BeEquivalentTo(workOrders);
        }

        [Fact]
        public async Task GetWorkOrders_ShouldDecorateWorkOrdersWithLines()
        {
            // Arrange
            List<WorkOrderModel>? workOrders = [
                new WorkOrderModel { Id = 1, LineId = 1, Description = "Test WorkOrder 1", CreateDate = DateTime.MinValue },
                new WorkOrderModel { Id = 2, LineId = 2, Description = "Test WorkOrder 2", CreateDate = DateTime.MinValue }
            ];
            List<LineModel>? lines = [
                new LineModel { Id = 1, Name = "Line 1" },
                new LineModel { Id = 2, Name = "Line 2" }
            ];

            _workOrderStoreMock.GetDataAsync().Returns(workOrders);
            _lineStoreMock.GetDataAsync().Returns(lines);

            // Act
            IEnumerable<WorkOrderModel>? result = await _workOrdersListRoot.GetWorkOrders();

            // Assert
            result.Should().HaveCount(2);
            result.FirstOrDefault(x => x.Id == 1)!.Line!.Name.Should().Be("Line 1");
            result.FirstOrDefault(x => x.Id == 2)!.Line!.Name.Should().Be("Line 2");
        }

        [Fact]
        public async Task GetWorkOrders_ShouldRaiseDecoratingCompletedEvent()
        {
            // Arrange
            List<WorkOrderModel>? workOrders = [
                new WorkOrderModel { Id = 1, LineId = 1, Description = "Test WorkOrder 1", CreateDate = DateTime.MinValue }
            ];
            List<LineModel>? lines = [
                new LineModel { Id = 1, Name = "Line 1" }
            ];

            _workOrderStoreMock.GetDataAsync().Returns(workOrders);
            _lineStoreMock.GetDataAsync().Returns(lines);

            bool eventRaised = false;
            _workOrdersListRoot.DecoratingCompleted += (sender, e) => eventRaised = true;

            // Act
            await _workOrdersListRoot.GetWorkOrders();

            // Assert
            eventRaised.Should().BeTrue();
        }

        [Fact]
        public async Task GetWorkOrders_ShouldAssignNullLine_WhenLineDoesNotExist()
        {
            // Arrange
            List<WorkOrderModel>? workOrders = [
                new WorkOrderModel { Id = 1, LineId = 99, Description = "Test WorkOrder 1", CreateDate = DateTime.MinValue }
            ];
            List<LineModel>? lines = [
                new LineModel { Id = 1, Name = "Line 1" }
            ];

            _workOrderStoreMock.GetDataAsync().Returns(workOrders);
            _lineStoreMock.GetDataAsync().Returns(lines);

            // Act
            IEnumerable<WorkOrderModel>? result = await _workOrdersListRoot.GetWorkOrders();

            // Assert
            result.First().Line.Should().BeNull();
        }
    }
}
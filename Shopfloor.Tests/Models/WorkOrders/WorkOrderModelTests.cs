using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Tests.Models.WorkOrders
{
    public class WorkOrderModelTests
    {
        [Fact]
        public void WorkOrderModel_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            DateTime createDate = new(2024, 9, 10);
            WorkOrderModel? workOrder = new()
            {
                Id = 1,
                LineId = 10,
                CreateDate = createDate
            };

            // Act
            // No action needed for default values check

            // Assert
            workOrder.Id.Should().Be(1);
            workOrder.Description.Should().BeEmpty();
            workOrder.Parts.Should().BeEmpty();
            workOrder.PartsId.Should().BeEmpty();
            workOrder.Line.Should().BeNull();
            workOrder.LineId.Should().Be(10);
            workOrder.CreateDate.Should().Be(createDate);
            workOrder.CreateDateOnlyDate.Should().Be(DateOnly.FromDateTime(createDate));
        }

        [Fact]
        public void WorkOrderModel_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string description = "Test Description";
            PartModel? part = new() { Id = 1, Name = "Part1" };
            LineModel? line = new() { Id = 10, Name = "Line10" };
            DateTime createDate = new(2024, 9, 10);
            WorkOrderModel? workOrder = new()
            {
                Id = 1,
                Description = description,
                Parts = [part],
                PartsId = [1],
                Line = line,
                LineId = 10,
                CreateDate = createDate
            };

            // Act
            // No action needed for value assignment check

            // Assert
            workOrder.Id.Should().Be(1);
            workOrder.Description.Should().Be(description);
            workOrder.Parts.Should().ContainSingle().Which.Should().BeEquivalentTo(part);
            workOrder.PartsId.Should().ContainSingle().Which.Should().Be(1);
            workOrder.Line.Should().BeEquivalentTo(line);
            workOrder.LineId.Should().Be(10);
            workOrder.CreateDate.Should().Be(createDate);
            workOrder.CreateDateOnlyDate.Should().Be(DateOnly.FromDateTime(createDate));
        }
    }
}
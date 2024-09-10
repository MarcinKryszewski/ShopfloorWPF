using Shopfloor.Models;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Tests.Models.WorkOrders
{
    public class WorkOrderDtoTests
    {
        [Fact]
        public void WorkOrderDto_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            WorkOrderDto? workOrderDto = new();

            // Act
            // No action needed for initialization check

            // Assert
            workOrderDto.Description.Should().BeEmpty();
            workOrderDto.Parts.Should().BeEmpty();
            workOrderDto.PartsId.Should().BeEmpty();
            workOrderDto.Line.Should().BeNull();
            workOrderDto.LineId.Should().Be(0);
            workOrderDto.CreateDate.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void WorkOrderDto_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            PartModel? part = new() { Id = 1, Name = "Part1" };
            LineModel? line = new() { Id = 10, Name = "Line10" };
            WorkOrderDto? workOrderDto = new()
            {
                Description = "Test Description",
                Parts = [part],
                PartsId = [1],
                Line = line,
                LineId = 10,
                CreateDate = new DateTime(2024, 9, 10)
            };

            // Act
            // No action needed for value assignment check

            // Assert
            workOrderDto.Description.Should().Be("Test Description");
            workOrderDto.Parts.Should().ContainSingle().Which.Should().BeEquivalentTo(part);
            workOrderDto.PartsId.Should().ContainSingle().Which.Should().Be(1);
            workOrderDto.Line.Should().BeEquivalentTo(line);
            workOrderDto.LineId.Should().Be(10);
            workOrderDto.CreateDate.Should().Be(new DateTime(2024, 9, 10));
        }
    }
}
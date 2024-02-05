using FluentAssertions;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Tests.Models.MachinesParts
{
    public sealed class MachinePartTests
    {
        [Fact]
        public void Part_WhenUsed_ShouldReturnPartType()
        {
            // Arrange
            Part part = new("test", null, null, null, null, null, null, null);
            Machine machine = new("test", "sdf435", null, null, true);
            MachinePart machinePart = new(part, machine);
            // Act
            Part? result = machinePart.Part;
            // Assert
            result.Should().BeOfType<Part>();
        }
        [Fact]
        public void Part_WhenUsed_ShouldNotBeNull()
        {
            // Arrange
            Part part = new("test", null, null, null, null, null, null, null);
            Machine machine = new("test", "sdf435", null, null, true);
            MachinePart machinePart = new(part, machine);
            // Act
            Part? result = machinePart.Part;
            // Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public void Part_WhenUsed_ShouldBeEqualToPart()
        {
            // Arrange
            Part part = new("test", null, null, null, null, null, null, null);
            Machine machine = new("test", "sdf435", null, null, true);
            MachinePart machinePart = new(part, machine);
            // Act
            bool? result = machinePart.Part?.Equals(part);
            // Assert
            result.Should().BeTrue();
            result.Should().NotBeNull();
        }
        [Fact]
        public void Machine_WhenUsed_ShouldNotBeNull()
        {
            // Arrange
            Part part = new("test", null, null, null, null, null, null, null);
            Machine machine = new("test", "sdf435", null, null, true);
            MachinePart machinePart = new(part, machine);
            // Act
            Machine? result = machinePart.Machine;
            // Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public void Machine_WhenUsed_ShouldBeEqualToPart()
        {
            // Arrange
            Part part = new("test", null, null, null, null, null, null, null);
            Machine machine = new("test", "sdf435", null, null, true);
            MachinePart machinePart = new(part, machine);
            // Act
            bool? result = machinePart.Machine?.Equals(machine);
            // Assert
            result.Should().BeTrue();
            result.Should().NotBeNull();
        }
    }
}
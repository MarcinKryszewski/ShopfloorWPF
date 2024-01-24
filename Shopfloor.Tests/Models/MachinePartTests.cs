using FluentAssertions;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Tests.Models
{
    public sealed class MachinePartTests
    {
        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(0.1)]
        [InlineData(0.000000001)]
        public void Amount_WhenPassedInConstructor_ShouldReturnValue(double amount)
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            MachinePart machinePart = new(partId, machineId, amount);
            // Act
            double? result = machinePart.Amount;
            // Assert
            result.Should().Be(amount);
        }
        [Fact]
        public void Amount_WhenNotPassedInConstructor_ShouldReturnDefaultValue()
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            MachinePart machinePart = new(partId, machineId, unit: "test");
            // Act
            double? result = machinePart.Amount;
            // Assert
            result.Should().Be(1);
        }
        [Fact]
        public void Amount_WhenNull_ShouldReturnDefaultValue()
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            MachinePart machinePart = new(partId, machineId, null, "test");
            // Act
            double? result = machinePart.Amount;
            // Assert
            result.Should().Be(1);
        }
        [Theory]
        [InlineData("normalString")]
        [InlineData("StringWithSpecialCharacters!@#$%^&*()")]
        [InlineData("StringWithNumbers1234567890")]
        [InlineData("zażółćgęśląjaźń")]
        [InlineData("ZAŻÓŁĆGĘŚLĄJAŹŃ")]
        [InlineData(" string ")]
        public void Unit_WhenPassedInConstructor_ShouldReturnValue(string unit)
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            MachinePart machinePart = new(partId, machineId, unit: unit);
            // Act
            string? result = machinePart.Unit;
            // Assert
            result.Should().Be(unit.Trim());
            result.Should().NotBeNullOrEmpty();
            result.Should().NotEndWith(" ");
            result.Should().NotStartWith(" ");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData(" \t\n")]
        public void Unit_WhenPassedNullOrEmpty_ShouldReturnDefaultValue(string unit)
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            Part part = new("test", null, null, null, null, null, null, null);
            MachinePart machinePart = new(partId, machineId, unit: unit);
            // Act
            string? result = machinePart.Unit;
            // Assert
            result.Should().Be(MachinePart.DefaultUnit);
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Unit_WhenNotPassedInConstructor_ShouldReturnDefaultValue()
        {
            // Arrange
            int partId = 5;
            int machineId = 3;
            MachinePart machinePart = new(partId, machineId);
            // Act
            string? result = machinePart.Unit;

            // Assert
            result.Should().Be(MachinePart.DefaultUnit);
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }
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
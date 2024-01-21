using FluentAssertions;
using Shopfloor.Models;

namespace Shopfloor.Tests.Models
{
    public class SupplierTests
    {
        [Theory]
        [InlineData(1, "Test")]
        [InlineData(999, "999")]
        [InlineData(1, "")]
        public void GetHashCode_ConstructorWithId_HashCodeOfId(int id, string name)
        {
            //Arrange
            Supplier supplier = new(id, name, true);

            //Act
            int hashCode = supplier.GetHashCode();

            //Assert
            hashCode.Should().Be(id.GetHashCode());
            hashCode.Should().NotBe(name.GetHashCode());
        }

        [Theory]
        [InlineData(1, "Test")]
        [InlineData(999, "999")]
        [InlineData(0, "")]
        public void GetHashCode_ConstructorWithoutId_HashCodeOfName(int id, string name)
        {
            //Arrange
            Supplier supplier = new(name, true);

            //Act
            int hashCode = supplier.GetHashCode();

            //Assert
            hashCode.Should().Be(name.GetHashCode());
            hashCode.Should().NotBe(id.GetHashCode());
            hashCode.Should().NotBe(0.GetHashCode());
        }

        [Theory]
        [InlineData(1, "Test", "Test2")]
        [InlineData(1, "Test", "Test")]
        public void Equals_ObjectWithSameId_ReturnTrue(int id, string name1, string name2)
        {
            //Arrange
            Supplier supplier = new(id, name1, true);
            Supplier supplierCompared = new(id, name2, true);

            //Act
            bool result = supplier.Equals(supplierCompared);
            bool resultReverser = supplierCompared.Equals(supplier);

            //Assert
            result.Should().BeTrue();
            resultReverser.Should().BeTrue();
        }

        [Theory]
        [InlineData(1, "Test", "Test2")]
        [InlineData(1, "Test", "Test")]
        public void Equals_OneObjectWithoutId_ReturnFalse(int id, string name1, string name2)
        {
            //Arrange
            Supplier supplier = new(name1, true);
            Supplier supplierCompared = new(id, name2, true);

            //Act
            bool result = supplier.Equals(supplierCompared);
            bool resultReverser = supplierCompared.Equals(supplier);

            //Assert
            result.Should().BeFalse();
            resultReverser.Should().BeFalse();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("")]
        public void Equals_BothObjectsWithoutIdsSameProperties_ReturnTrue(string name)
        {
            //Arrange
            Supplier supplier = new(name, true);
            Supplier supplierCompared = new(name, true);

            //Act
            bool result = supplier.Equals(supplierCompared);
            bool resultReverser = supplierCompared.Equals(supplier);

            //Assert
            result.Should().BeTrue();
            resultReverser.Should().BeTrue();
        }

        [Theory]
        [InlineData("Test", "Test2")]
        [InlineData("Test", "")]
        public void Equals_BothWithoutIdsDifferentProperties_ReturnFalse(string name1, string name2)
        {
            //Arrange
            Supplier supplier = new(name1, true);
            Supplier supplierCompared = new(name2, true);

            //Act
            bool result = supplier.Equals(supplierCompared);
            bool resultReverser = supplierCompared.Equals(supplier);

            //Assert
            result.Should().BeFalse();
            resultReverser.Should().BeFalse();
        }

        [Fact]
        public void Equals_NullObject_ReturnsFalse()
        {
            // Arrange
            Supplier supplier = new("Test", true);

            // Act
            bool result = supplier.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_ObjectWithDifferentType_ReturnsFalse()
        {
            // Arrange
            Supplier supplier = new("Test", true);

            // Act
            bool result = supplier.Equals("not a Supplier");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Supplier")]
        public void SearchValue_WithName_ReturnsName(string name)
        {
            // Arrange
            Supplier supplier = new(1, name, true);

            // Act
            string result = supplier.SearchValue;

            // Assert
            result.Should().Be(name);
        }

        [Fact]
        public void SearchValue_WithEmptyName_ReturnsEmptyString()
        {
            // Arrange
            Supplier supplier = new(string.Empty, true);

            // Act
            string result = supplier.SearchValue;

            // Assert
            result.Should().Be(string.Empty);
            result.Should().HaveLength(0);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsActive_ShouldReturnValue_Boolean(bool value)
        {
            // Arrange
            Supplier supplier = new("Test", value);

            // Act
            bool result = supplier.IsActive;

            // Assert
            result.Should().Be(value);
            result.Should().NotBe(!value);
        }
    }
}
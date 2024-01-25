using FluentAssertions;
using Shopfloor.Models.PartTypeModel;

namespace Shopfloor.Tests.Models.PartTypes
{
    public sealed class PartTypeTests
    {
        [Theory]
        [InlineData(1, "Test")]
        [InlineData(999, "999")]
        [InlineData(1, "")]
        public void GetHashCode_ConstructorWithId_HashCodeOfId(int id, string name)
        {
            //Arrange
            PartType partType = new(id, name);

            //Act
            int hashCode = partType.GetHashCode();

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
            PartType partType = new(name);

            //Act
            int hashCode = partType.GetHashCode();

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
            PartType partType = new(id, name1);
            PartType partTypeCompared = new(id, name2);

            //Act
            bool result = partType.Equals(partTypeCompared);
            bool resultReverser = partTypeCompared.Equals(partType);

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
            PartType partType = new(name1);
            PartType partTypeCompared = new(id, name2);

            //Act
            bool result = partType.Equals(partTypeCompared);
            bool resultReverser = partTypeCompared.Equals(partType);

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
            PartType partType = new(name);
            PartType partTypeCompared = new(name);

            //Act
            bool result = partType.Equals(partTypeCompared);
            bool resultReverser = partTypeCompared.Equals(partType);

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
            PartType partType = new(name1);
            PartType partTypeCompared = new(name2);

            //Act
            bool result = partType.Equals(partTypeCompared);
            bool resultReverser = partTypeCompared.Equals(partType);

            //Assert
            result.Should().BeFalse();
            resultReverser.Should().BeFalse();
        }
        [Fact]
        public void Equals_NullObject_ReturnsFalse()
        {
            // Arrange
            PartType partType = new("Test");

            // Act
            bool result = partType.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void Equals_ObjectWithDifferentType_ReturnsFalse()
        {
            // Arrange
            PartType partType = new("Test");

            // Act
            bool result = partType.Equals("not a PartType");

            // Assert
            result.Should().BeFalse();
        }
        [Theory]
        [InlineData("PartType")]
        public void SearchValue_WithName_ReturnsName(string name)
        {
            // Arrange
            PartType partType = new(1, name);

            // Act
            string result = partType.SearchValue;

            // Assert
            result.Should().Be(name);
        }
        [Fact]
        public void SearchValue_WithEmptyName_ReturnsEmptyString()
        {
            // Arrange
            PartType partType = new(string.Empty);

            // Act
            string result = partType.SearchValue;

            // Assert
            result.Should().Be(string.Empty);
            result.Should().HaveLength(0);
        }
    }
}
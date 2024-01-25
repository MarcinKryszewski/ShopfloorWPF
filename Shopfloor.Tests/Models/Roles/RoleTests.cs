using FluentAssertions;
using Shopfloor.Models.RoleModel;

namespace Shopfloor.Tests.Models.Roles
{
    public sealed class RoleTests
    {
        internal sealed class TestDataWithoutId : TheoryData<Role>
        {
            public TestDataWithoutId()
            {
                Add(new Role("test", 1));
                Add(new Role(string.Empty, 0));
                Add(new Role("; Drop Database", -1));
            }
        }
        internal sealed class TestDataWithId : TheoryData<Role>
        {
            public TestDataWithId()
            {
                Add(new Role(1, "test", 1));
                Add(new Role(0, string.Empty, 0));
                Add(new Role(-1, "; Drop Database", -1));
            }
        }
        [Fact]
        public void Id_ShouldReturnNull_NotPassedIntoConstructor()
        {
            // Arrange
            Role role = new("test", 1);
            // Act
            int? result = role.Id;
            // Assert
            result.Should().BeNull();
        }
        [Theory]
        [ClassData(typeof(TestDataWithoutId))]
        public void Name_ConstructorWithoutId_ShouldReturnString(Role role)
        {
            // Act
            string result = role.Name;
            // Assert
            result.Should().Be(role.Name);
            result.Should().HaveLength(role.Name.Length);
        }
        [Theory]
        [ClassData(typeof(TestDataWithoutId))]
        public void Value_ConstructorWithoutId_ShouldReturnString(Role role)
        {
            // Act
            int result = role.Value;
            // Assert
            result.Should().Be(role.Value);
        }
        [Theory]
        [ClassData(typeof(TestDataWithId))]
        public void Name_ConstructorWithId_ShouldReturnString(Role role)
        {
            // Act
            string result = role.Name;
            // Assert
            result.Should().Be(role.Name);
            result.Should().HaveLength(role.Name.Length);
        }
        [Theory]
        [ClassData(typeof(TestDataWithId))]
        public void Value_ConstructorWithId_ShouldReturnString(Role role)
        {
            // Act
            int result = role.Value;
            // Assert
            result.Should().Be(role.Value);
        }
        [Fact]
        public void Id_ConstructorWithId_ShouldNotBeNull()
        {
            // Arrange
            Role role = new(5, string.Empty, 5);
            // Act
            int? result = role.Id;
            // Assert
            result.Should().NotBeNull();
        }
        [Theory]
        [ClassData(typeof(TestDataWithId))]
        public void Id_ConstructWithId_ShouldReturnInteger(Role role)
        {
            // Act
            int? result = role.Id;
            // Assert
            result.Should().Be(role.Id);
        }
    }
}
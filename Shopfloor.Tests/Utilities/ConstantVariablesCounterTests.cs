using FluentAssertions;
using Shopfloor.Utilities;

namespace Shopfloor.Tests.Utilities
{
    public class ConstantVariablesCounterTests
    {
        private sealed class TestClass
        {
            //fields are used to test their amount
#pragma warning disable IDE0051 // Remove unused private members
            private const string stringConst = "stringConst";
            private const int intConst = 3;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0052 // Remove unread private members
            private readonly string _stringVariable = string.Empty;
#pragma warning restore IDE0052 // Remove unread private members

        }
        [Fact]
        public void CountConstantVariablesOfClass_ShouldReturnValue_WhenClassProvided()
        {
            // Arrange            
            // Act
            int? result = ConstantVariablesCounter.CountConstantVariablesOfClass<TestClass>();
            // Assert
            result.Should().Be(2);
            result.Should().NotBeNull();
        }
    }
}
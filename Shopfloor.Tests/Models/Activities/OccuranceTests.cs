using Shopfloor.Models.Activities;

namespace Shopfloor.Tests.Models.Activities
{
    public class OccuranceTests
    {
        [Theory]
        [InlineData(OccuranceUnit.D)]
        [InlineData(OccuranceUnit.W)]
        [InlineData(OccuranceUnit.M)]
        [InlineData(OccuranceUnit.Y)]
        public void Occurance_ShouldBeEqualToOccurance(OccuranceUnit unit)
        {
            // Arrange
            Occurance aaa = new()
            {
                Unit = unit,
            };
            Occurance bbb = new()
            {
                Unit = unit,
            };

            // Act
            bool result = aaa.Equals(bbb);

            // Assert
            result.Should().BeTrue();
        }
    }
}
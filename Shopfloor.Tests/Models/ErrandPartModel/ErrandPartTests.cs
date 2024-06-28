using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Tests.Models.ErrandPartModel
{
    public class ErrandPartTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        public void Id_ShouldNotAddError_WhenAssigningId(int? id)
        {
            // Arrange
            ErrandPart errandPart = new() { ErrandId = 1, PartId = 1 };

            // Act
            errandPart.Id = id;
            int result = errandPart.CountErrors(nameof(errandPart.Id));

            // Assert
            result.Should().Be(0);
        }
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void Id_ShouldNotAddError_WhenIsNullOrZero(int? id)
        {
            // Arrange
            int testId = 1;
            ErrandPart errandPart = new() { ErrandId = 1, PartId = 1, Id = id };
            // Act
            errandPart.Id = testId;
            int result = errandPart.CountErrors(nameof(errandPart.Id));

            // Assert
            result.Should().Be(0);
        }
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, null)]
        [InlineData(3, 0)]
        public void Id_ShouldAddError_WhenAssigned(int? startId, int? newId)
        {
            // Arrange
            ErrandPart errandPart = new(startId) { ErrandId = 1, PartId = 1 };
            // Act
            errandPart.Id = newId;
            int result = errandPart.CountErrors(nameof(errandPart.Id));
            // Assert
            result.Should().Be(1);
            result.Should().NotBe(0);
        }
    }
}

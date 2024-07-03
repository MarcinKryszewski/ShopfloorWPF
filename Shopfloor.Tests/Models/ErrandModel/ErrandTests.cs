using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Tests.Models.ErrandModel
{
    public class ErrandTests
    {
        [Fact]
        public void Clone_IfCloned_PartsListShouldBeSeparateFromOriginal()
        {
            // Arrange
            ErrandPart part1 = new() { ErrandId = 1, PartId = 1 };
            ErrandPart part2 = new() { ErrandId = 1, PartId = 2 };
            Errand errandOriginal = new() { Id = 1, CreatedById = 1 };
            errandOriginal.Parts.Add(part1);

            // Act
            Errand clone = (Errand)errandOriginal.Clone();
            clone.Parts.Add(part2);
            int result = clone.Parts.Count;

            // Assert
            result.Should().NotBe(errandOriginal.Parts.Count);
            result.Should().Be(2);
        }
    }
}

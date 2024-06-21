using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;

namespace Shopfloor.Tests.Models.ErrandModel.Services
{
    public class ErrandDatabaseServiceTests
    {
        [Fact]
        public async Task AddErrandToDatabase_ShouldReturnExpectedId()
        {
            // Arrange
            int errandId = 1;
            Errand errand = new()
            {
                Description = "Test Errand",
                CreatedById = 1
            };
            IProvider<Errand> provider = Substitute.For<IProvider<Errand>>();
            provider.Create(errand).Returns(Task.FromResult(errandId));

            ErrandDatabaseService service = new(provider);

            // Act
            int result = await Task.Run(() => service.AddErrandToDatabase(errand));

            // Assert
            result.Should().Be(errandId);
            await provider.Received().Create(errand);
        }
    }
}
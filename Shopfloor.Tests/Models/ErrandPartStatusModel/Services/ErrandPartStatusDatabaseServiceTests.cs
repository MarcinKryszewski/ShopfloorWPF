using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Services;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel.Services
{
    public class ErrandPartStatusDatabaseServiceTests
    {
        private readonly IProvider<ErrandPartStatus> _provider;
        private readonly ErrandPartStatusDatabaseService _service;

        public ErrandPartStatusDatabaseServiceTests()
        {
            _provider = Substitute.For<IProvider<ErrandPartStatus>>();
            _service = new(_provider);
        }

        [Fact]
        public async Task AddToDatabase_ShouldReturnId()
        {
            // Arrange
            ErrandPartStatus errandPartStatus = new()
            {
                ErrandPartId = 1,
                CreatedDate = DateTime.Now,
            };
            int generatedId = 123;
            _provider.Create(errandPartStatus).Returns(Task.FromResult(generatedId));

            // Act
            int result = _service.AddToDatabase(errandPartStatus);

            // Assert
            result.Should().Be(generatedId);
            await _provider.Received(1).Create(errandPartStatus);
        }
    }
}
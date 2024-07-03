using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;

namespace Shopfloor.Tests.Models.ErrandPartModel.Services
{
    public class ErrandPartDatabaseServiceTests
    {
        private readonly IProvider<ErrandPart> _provider;
        private readonly ErrandPartDatabaseService _service;

        public ErrandPartDatabaseServiceTests()
        {
            _provider = Substitute.For<IProvider<ErrandPart>>();
            _service = new(_provider);
        }

        [Fact]
        public async Task AddToDatabase_ShouldReturnId()
        {
            // Arrange
            ErrandPart errandPart = new()
            {
                ErrandId = 1,
                PartId = 1
            };
            int generatedId = 123;
            _provider.Create(errandPart).Returns(Task.FromResult(generatedId));

            // Act
            int result = _service.Add(errandPart);

            // Assert
            result.Should().Be(generatedId);
            await _provider.Received(1).Create(errandPart);
        }
    }
}
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;
using Shopfloor.Models.ErrandPartStatusModel;

namespace Shopfloor.Tests.Models.ErrandPartModel.Services
{
    public class ErrandPartCreatorServiceTests
    {
        private readonly IDataModelDatabaseService<ErrandPart> _databaseService;
        private readonly IDataModelStoreService<ErrandPart> _storeService;
        private readonly IModelCreatorService<ErrandPartStatus> _statusCreator;
        private readonly ErrandPartCreatorService _service;

        public ErrandPartCreatorServiceTests()
        {
            _databaseService = Substitute.For<IDataModelDatabaseService<ErrandPart>>();
            _storeService = Substitute.For<IDataModelStoreService<ErrandPart>>();
            _statusCreator = Substitute.For<IModelCreatorService<ErrandPartStatus>>();

            _service = new ErrandPartCreatorService(
                _databaseService,
                _storeService,
                _statusCreator);
        }

        [Fact]
        public void Create_ShouldAssignId()
        {
            // Arrange
            ErrandPart errandPart = new()
            {
                ErrandId = 1,
                PartId = 1
            };
            int generatedId = 123;
            _databaseService.Add(errandPart).Returns(generatedId);

            // Act
            _service.Create(errandPart);
            int? result = errandPart.Id;

            // Assert
            result.Should().Be(generatedId);
            result.Should().NotBe(null);
        }
        [Fact]
        public void Create_ShouldItemAddToStore()
        {
            // Arrange
            ErrandPart errandPart = new()
            {
                ErrandId = 1,
                PartId = 1
            };

            // Act
            _service.Create(errandPart);

            // Assert
            _databaseService.Received(1).Add(errandPart);
            _storeService.Received(1).Add(errandPart);
        }
        [Fact]
        public void Create_ShouldAddToStoreWithPersistedId()
        {
            // Arrange
            ErrandPart errandPart = new()
            {
                ErrandId = 1,
                PartId = 1
            };
            int generatedId = 123;
            _databaseService.Add(errandPart).Returns(generatedId);

            // Act
            _service.Create(errandPart);

            // Assert
            _storeService.Received(1).Add(Arg.Is<ErrandPart>(e => e.Id == generatedId));
        }
    }
}
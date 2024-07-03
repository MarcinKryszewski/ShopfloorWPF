using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Services;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel.Services
{
    public class ErrandPartStatusCreatorServiceTests
    {
        private readonly IDataModelDatabaseService<ErrandPartStatus> _databaseService;
        private readonly IDataModelStoreService<ErrandPartStatus> _storeService;
        private readonly ErrandPartStatusCreatorService _service;

        public ErrandPartStatusCreatorServiceTests()
        {
            _databaseService = Substitute.For<IDataModelDatabaseService<ErrandPartStatus>>();
            _storeService = Substitute.For<IDataModelStoreService<ErrandPartStatus>>();

            _service = new ErrandPartStatusCreatorService(
                _storeService,
                _databaseService);
        }

        [Fact]
        public void Create_ShouldAssignId()
        {
            // Arrange
            ErrandPartStatus errandPartStatus = new()
            {
                ErrandPartId = 1,
                CreatedDate = DateTime.Now,
            };
            int generatedId = 123;
            _databaseService.Add(errandPartStatus).Returns(generatedId);

            // Act
            _service.Create(errandPartStatus);
            int? result = errandPartStatus.Id;

            // Assert
            result.Should().Be(generatedId);
            result.Should().NotBe(null);
        }
        [Fact]
        public void Create_ShouldItemAddToStore()
        {
            // Arrange
            ErrandPartStatus errandPartStatus = new()
            {
                ErrandPartId = 1,
                CreatedDate = DateTime.Now,
            };

            // Act
            _service.Create(errandPartStatus);

            // Assert
            _databaseService.Received(1).Add(errandPartStatus);
            _storeService.Received(1).Add(errandPartStatus);
        }
        [Fact]
        public void Create_ShouldAddToStoreWithPersistedId()
        {
            // Arrange
            ErrandPartStatus errandPartStatus = new()
            {
                ErrandPartId = 1,
                CreatedDate = DateTime.Now,
            };
            int generatedId = 123;
            _databaseService.Add(errandPartStatus).Returns(generatedId);

            // Act
            _service.Create(errandPartStatus);

            // Assert
            _storeService.Received(1).Add(Arg.Is<ErrandPartStatus>(e => e.Id == generatedId));
        }
    }
}
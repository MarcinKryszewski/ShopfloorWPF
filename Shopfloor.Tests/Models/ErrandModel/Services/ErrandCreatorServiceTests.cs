using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;

namespace Shopfloor.Tests.Models.ErrandModel.Services
{
    public class ErrandCreatorServiceTests
    {
        [Fact]
        public void Create_ShouldDecorateErrandWithId()
        {
            // Arrange
            int testId = 69;
            Errand errand = new() { CreatedById = 1 };

            IDataModelDatabaseService<Errand> databaseService = Substitute.For<IDataModelDatabaseService<Errand>>();
            IDataModelStoreService<Errand> storeService = Substitute.For<IDataModelStoreService<Errand>>();
            IModelCreatorService<ErrandStatus> statusCreator = Substitute.For<IModelCreatorService<ErrandStatus>>();
            databaseService.AddToDatabase(errand).Returns(testId);

            ErrandCreatorService sut = new(databaseService, storeService, statusCreator);

            // Act
            sut.Create(errand);
            int? result = errand.Id;

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(testId);
        }
    }
}
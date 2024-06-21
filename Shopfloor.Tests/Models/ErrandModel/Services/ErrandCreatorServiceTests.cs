using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;

namespace Shopfloor.Tests.Models.ErrandModel.Services
{
    public class ErrandCreatorServiceTests
    {
        private ErrandCreatorService _sut;
        private IErrandDatabaseService _databaseService;
        private IErrandStoreService _storeService;
        private IErrandStatusDatabaseService _statusDatabaseService;
        private IErrandStatusStoreService _statusStoreService;
        private Errand _errand;

        public ErrandCreatorServiceTests()
        {
            _databaseService = Substitute.For<IErrandDatabaseService>();
            _storeService = Substitute.For<IErrandStoreService>();
            _statusDatabaseService = Substitute.For<IErrandStatusDatabaseService>();
            _statusStoreService = Substitute.For<IErrandStatusStoreService>();

            _sut = new(
                _databaseService,
                _storeService,
                _statusDatabaseService,
                _statusStoreService
            );

            _errand = new() { CreatedById = 1 };

            _databaseService.AddErrandToDatabase(_errand).Returns(1);
            _statusDatabaseService.AddErrandStatusToDatabase(Arg.Any<ErrandStatus>()).Returns(1);

        }
        [Fact]
        public void Create_ShouldAddErrandToDatabase()
        {
            // Act
            _sut.Create(_errand);

            // Assert
            _databaseService.Received(1).AddErrandToDatabase(_errand);
        }

        [Fact]
        public void Create_ShouldAddErrandToStore()
        {
            // Act
            _sut.Create(_errand);

            // Assert
            _storeService.Received(1).AddErrandToStore(_errand);
        }

        [Fact]
        public void Create_ShouldAddErrandStatusToDatabase()
        {
            // Act
            _sut.Create(_errand);

            // Assert
            _statusDatabaseService.Received(1).AddErrandStatusToDatabase(Arg.Any<ErrandStatus>());
        }

        [Fact]
        public void Create_ShouldAddErrandStatusToStore()
        {
            // Act
            _sut.Create(_errand);

            // Assert
            _statusStoreService.Received(1).AddErrandStatusToStore(Arg.Any<ErrandStatus>());
        }

        [Fact]
        public void Create_ShouldUpdateErrandIdInStore()
        {
            // Act
            _sut.Create(_errand);

            // Assert
            _errand.Id.Should().Be(1);
            _storeService.Received(1).AddErrandToStore(_errand);
        }
    }
}
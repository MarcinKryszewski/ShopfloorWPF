using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;

namespace Shopfloor.Tests.Models.ErrandPartModel.Services
{
    public class ErrandPartStoreServiceTests
    {
        private readonly IDataStore<ErrandPart> _store;
        private readonly ErrandPartStoreService _service;

        public ErrandPartStoreServiceTests()
        {
            _store = Substitute.For<IDataStore<ErrandPart>>();
            _service = new ErrandPartStoreService(_store);
        }

        [Fact]
        public void AddToStore_ShouldAddItemToDataStore()
        {
            // Arrange
            ErrandPart errandPart = new()
            {
                ErrandId = 1,
                PartId = 1
            };
            _store.Data.Returns([]);

            // Act
            _service.Add(errandPart);

            // Assert
            _store.Data.Should().Contain(errandPart);
        }
    }
}
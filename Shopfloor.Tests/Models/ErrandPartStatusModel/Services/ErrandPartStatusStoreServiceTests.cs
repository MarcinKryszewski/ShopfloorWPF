using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Services;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel.Services
{
    public class ErrandPartStatusStoreServiceTests
    {
        private readonly IDataStore<ErrandPartStatus> _store;
        private readonly ErrandPartStatusStoreService _service;

        public ErrandPartStatusStoreServiceTests()
        {
            _store = Substitute.For<IDataStore<ErrandPartStatus>>();
            _service = new(_store);
        }

        [Fact]
        public void AddToStore_ShouldAddItemToDataStore()
        {
            // Arrange
            ErrandPartStatus errandPartStatus = new()
            {
                ErrandPartId = 1,
                CreatedDate = DateTime.Now,
            };
            _store.Data.Returns([]);

            // Act
            _service.AddToStore(errandPartStatus);

            // Assert
            _store.Data.Should().Contain(errandPartStatus);
        }
    }
}
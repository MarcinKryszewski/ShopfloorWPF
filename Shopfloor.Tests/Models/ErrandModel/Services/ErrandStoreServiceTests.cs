using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;

namespace Shopfloor.Tests.Models.ErrandModel.Services
{
    public class ErrandStoreServiceTests
    {
        [Fact]
        public void AddErrandToStore_ShouldAddErrandToDataStore()
        {
            // Arrange
            Errand errand = new()
            {
                Id = 1,
                Description = "Test Errand",
                CreatedById = 1
            };
            IDataStore<Errand> dataStore = Substitute.For<IDataStore<Errand>>();
            dataStore.Data.Returns([]);

            ErrandStoreService service = new(dataStore);

            // Act
            service.AddToStore(errand);
            int result = dataStore.Data.Count;

            // Assert
            dataStore.Data.Should().ContainSingle().Which.Should().BeEquivalentTo(errand);
            result.Should().Be(1);
        }
    }
}
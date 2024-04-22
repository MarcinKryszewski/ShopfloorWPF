using NSubstitute;
using Shopfloor.Database;
using Shopfloor.Models.ErrandPartModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel
{
    public class ErrandPartProviderTests
    {
        private DatabaseConnectionFactory subDatabaseConnectionFactory;

        public ErrandPartProviderTests()
        {
            this.subDatabaseConnectionFactory = Substitute.For<DatabaseConnectionFactory>();
        }

        private ErrandPartProvider CreateProvider()
        {
            return new ErrandPartProvider(
                this.subDatabaseConnectionFactory);
        }

        [Fact]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            ErrandPart item = null;

            // Act
            var result = await provider.Create(
                item);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();

            // Act
            var result = await provider.GetAll();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetByErrandId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int errandId = 0;

            // Act
            var result = await provider.GetByErrandId(
                errandId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int id = 0;

            // Act
            var result = await provider.GetById(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            ErrandPart item = null;

            // Act
            await provider.Update(
                item);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdatePrice_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int id = 0;
            double pricePerUnit = 0;

            // Act
            await provider.UpdatePrice(
                id,
                pricePerUnit);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateDeliveryDate_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int id = 0;
            DateTime? expectedDeliveryDate = null;

            // Act
            await provider.UpdateDeliveryDate(
                id,
                expectedDeliveryDate);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateDeliveryDate_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var provider = this.CreateProvider();
            int id = 0;
            bool cancel = false;

            // Act
            await provider.UpdateDeliveryDate(
                id,
                cancel);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int id = 0;

            // Act
            await provider.Delete(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var provider = this.CreateProvider();
            int errandId = 0;
            int partId = 0;

            // Act
            await provider.Delete(
                errandId,
                partId);

            // Assert
            Assert.True(false);
        }
    }
}

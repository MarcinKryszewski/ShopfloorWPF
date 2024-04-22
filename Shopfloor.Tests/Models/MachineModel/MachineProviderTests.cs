using NSubstitute;
using Shopfloor.Database;
using Shopfloor.Models.MachineModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel
{
    public class MachineProviderTests
    {
        private DatabaseConnectionFactory subDatabaseConnectionFactory;

        public MachineProviderTests()
        {
            this.subDatabaseConnectionFactory = Substitute.For<DatabaseConnectionFactory>();
        }

        private MachineProvider CreateProvider()
        {
            return new MachineProvider(
                this.subDatabaseConnectionFactory);
        }

        [Fact]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            Machine item = null;

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
            Machine item = null;

            // Act
            await provider.Update(
                item);

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
    }
}

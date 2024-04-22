using NSubstitute;
using Shopfloor.Database;
using Shopfloor.Models.RoleUserModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.RoleUserModel
{
    public class RoleIUserProviderTests
    {
        private DatabaseConnectionFactory subDatabaseConnectionFactory;

        public RoleIUserProviderTests()
        {
            this.subDatabaseConnectionFactory = Substitute.For<DatabaseConnectionFactory>();
        }

        private RoleIUserProvider CreateProvider()
        {
            return new RoleIUserProvider(
                this.subDatabaseConnectionFactory);
        }

        [Fact]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            RoleUser item = null;

            // Act
            var result = await provider.Create(
                item);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Create_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var provider = this.CreateProvider();
            int RoleId = 0;
            int UserId = 0;

            // Act
            var result = await provider.Create(
                RoleId,
                UserId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int roleId = 0;
            int userId = 0;

            // Act
            await provider.Delete(
                roleId,
                userId);

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
        public async Task GetAllForUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int userId = 0;

            // Act
            var result = await provider.GetAllForUser(
                userId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAllForRole_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var provider = this.CreateProvider();
            int roleId = 0;

            // Act
            var result = await provider.GetAllForRole(
                roleId);

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
            RoleUser item = null;

            // Act
            await provider.Update(
                item);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior1()
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

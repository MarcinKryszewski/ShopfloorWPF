using NSubstitute;
using Shopfloor.Hosts.Database;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Database
{
    public class DatabaseHostTests
    {


        public DatabaseHostTests()
        {

        }

        private DatabaseHost CreateDatabaseHost()
        {
            return new DatabaseHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var databaseHost = this.CreateDatabaseHost();
            IServiceCollection services = null;

            // Act
            databaseHost.Get(
                services);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ModelServices_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var databaseHost = this.CreateDatabaseHost();
            IServiceCollection services = null;

            // Act
            databaseHost.ModelServices(
                services);

            // Assert
            Assert.True(false);
        }
    }
}

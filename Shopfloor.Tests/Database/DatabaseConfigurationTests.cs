using NSubstitute;
using Shopfloor.Database.Configuration;
using System;
using Xunit;

namespace Shopfloor.Tests.Database
{
    public class DatabaseConfigurationTests
    {


        public DatabaseConfigurationTests()
        {

        }

        private DatabaseConfiguration CreateDatabaseConfiguration()
        {
            return new DatabaseConfiguration();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var databaseConfiguration = this.CreateDatabaseConfiguration();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}

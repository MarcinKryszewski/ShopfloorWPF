using NSubstitute;
using Shopfloor.Database.Configuration;
using Shopfloor.Database.Initializers;
using System;
using System.Data;
using Xunit;

namespace Shopfloor.Tests.Database.Initializers
{
    public class DatabaseInitializerFactoryTests
    {
        private DatabaseConfiguration subDatabaseConfiguration;
        private IDbConnection subDbConnection;

        public DatabaseInitializerFactoryTests()
        {
            this.subDatabaseConfiguration = Substitute.For<DatabaseConfiguration>();
            this.subDbConnection = Substitute.For<IDbConnection>();
        }

        private DatabaseInitializerFactory CreateFactory()
        {
            return new DatabaseInitializerFactory(
                this.subDatabaseConfiguration,
                this.subDbConnection);
        }

        [Fact]
        public void CreateInitializer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var factory = this.CreateFactory();

            // Act
            var result = factory.CreateInitializer();

            // Assert
            Assert.True(false);
        }
    }
}

using NSubstitute;
using Shopfloor.Database.Initializers;
using System;
using System.Data;
using Xunit;

namespace Shopfloor.Tests.Database.Initializers
{
    public class SQLiteDatabaseInitializerTests
    {
        private IDbConnection subDbConnection;

        public SQLiteDatabaseInitializerTests()
        {
            this.subDbConnection = Substitute.For<IDbConnection>();
        }

        private SQLiteDatabaseInitializer CreateSQLiteDatabaseInitializer()
        {
            return new SQLiteDatabaseInitializer(
                this.subDbConnection,
                TODO);
        }

        [Fact]
        public void Initialize_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sQLiteDatabaseInitializer = this.CreateSQLiteDatabaseInitializer();

            // Act
            sQLiteDatabaseInitializer.Initialize();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void CreateDatabase_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sQLiteDatabaseInitializer = this.CreateSQLiteDatabaseInitializer();

            // Act
            sQLiteDatabaseInitializer.CreateDatabase();

            // Assert
            Assert.True(false);
        }
    }
}

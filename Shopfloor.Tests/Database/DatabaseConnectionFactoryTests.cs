using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Shopfloor.Database;
using System.Data.Common;

namespace Shopfloor.Tests.Database
{
    public class DatabaseConnectionFactoryTests
    {
        [Fact]
        public void Constructor_Should_Initialize_DatabaseType()
        {
            // Arrange
            string testValue = "testType";
            IConfiguration configuration = Substitute.For<IConfiguration>();
            configuration["DatabaseType"].Returns(testValue);

            // Act
            DatabaseConnectionFactory factory = new(configuration);

            // Assert
            factory.DatabaseType.Should().Be(testValue);
        }

        [Fact]
        public void Connect_Should_Return_SqliteConnection_When_DatabaseType_Is_SQLite()
        {
            // Arrange
            IConfiguration configuration = Substitute.For<IConfiguration>();
            configuration["DatabaseType"].Returns("SQLite");
            configuration.GetConnectionString("SQLiteConnection").Returns("DataSource=:memory:");
            DatabaseConnectionFactory factory = new(configuration);

            // Act
            DbConnection connection = factory.Connect();

            // Assert
            connection.Should().NotBeNull();
            connection.Should().BeOfType<SqliteConnection>();
        }

        [Fact]
        public void Connect_Should_Throw_InvalidOperationException_For_Unsupported_DatabaseType()
        {
            // Arrange
            IConfiguration unsupportedConfiguration = Substitute.For<IConfiguration>();
            unsupportedConfiguration["DatabaseType"].Returns("UnsupportedDatabase");
            DatabaseConnectionFactory factory = new(unsupportedConfiguration);

            // Act
            Action act = () => factory.Connect();

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Invalid or unsupported database type.");
        }
    }
}
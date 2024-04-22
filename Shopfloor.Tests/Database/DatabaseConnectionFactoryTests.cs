using Microsoft.Extensions.Configuration;
using NSubstitute;
using Shopfloor.Database;
using System;
using Xunit;

namespace Shopfloor.Tests.Database
{
    public class DatabaseConnectionFactoryTests
    {
        private IConfiguration subConfiguration;

        public DatabaseConnectionFactoryTests()
        {
            this.subConfiguration = Substitute.For<IConfiguration>();
        }

        private DatabaseConnectionFactory CreateFactory()
        {
            return new DatabaseConnectionFactory(
                this.subConfiguration);
        }

        [Fact]
        public void Connect_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var factory = this.CreateFactory();

            // Act
            var result = factory.Connect();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Dispose_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var factory = this.CreateFactory();

            // Act
            factory.Dispose();

            // Assert
            Assert.True(false);
        }
    }
}

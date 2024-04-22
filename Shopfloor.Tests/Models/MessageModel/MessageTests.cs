using NSubstitute;
using Shopfloor.Models.MessageModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MessageModel
{
    public class MessageTests
    {


        public MessageTests()
        {

        }

        private Message CreateMessage()
        {
            return new Message();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var message = this.CreateMessage();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}

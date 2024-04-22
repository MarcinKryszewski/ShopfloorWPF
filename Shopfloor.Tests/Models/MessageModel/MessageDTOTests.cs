using NSubstitute;
using Shopfloor.Models.MessageModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MessageModel
{
    public class MessageDTOTests
    {


        public MessageDTOTests()
        {

        }

        private MessageDTO CreateMessageDTO()
        {
            return new MessageDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var messageDTO = this.CreateMessageDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}

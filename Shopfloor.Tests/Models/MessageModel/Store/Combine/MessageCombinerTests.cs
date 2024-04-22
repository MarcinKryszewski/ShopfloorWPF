using NSubstitute;
using Shopfloor.Models.MessageModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.MessageModel.Store.Combine
{
    public class MessageCombinerTests
    {


        public MessageCombinerTests()
        {

        }

        private MessageCombiner CreateMessageCombiner()
        {
            return new MessageCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var messageCombiner = this.CreateMessageCombiner();
            bool shouldForce = false;

            // Act
            await messageCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}

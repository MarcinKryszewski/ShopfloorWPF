using NSubstitute;
using Shopfloor.Models.MessageModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MessageModel.Store
{
    public class MessageStoreTests
    {
        private MessageProvider subMessageProvider;

        public MessageStoreTests()
        {
            this.subMessageProvider = Substitute.For<MessageProvider>();
        }

        private MessageStore CreateMessageStore()
        {
            return new MessageStore(
                this.subMessageProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var messageStore = this.CreateMessageStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}

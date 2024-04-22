using NSubstitute;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store.Combine
{
    public class ErrandPartCombinerTests
    {
        private ErrandPartToUser subErrandPartToUser;
        private ErrandPartToPart subErrandPartToPart;
        private ErrandPartToErrand subErrandPartToErrand;
        private ErrandPartToErrandPartStatus subErrandPartToErrandPartStatus;

        public ErrandPartCombinerTests()
        {
            this.subErrandPartToUser = Substitute.For<ErrandPartToUser>();
            this.subErrandPartToPart = Substitute.For<ErrandPartToPart>();
            this.subErrandPartToErrand = Substitute.For<ErrandPartToErrand>();
            this.subErrandPartToErrandPartStatus = Substitute.For<ErrandPartToErrandPartStatus>();
        }

        private ErrandPartCombiner CreateErrandPartCombiner()
        {
            return new ErrandPartCombiner(
                this.subErrandPartToUser,
                this.subErrandPartToPart,
                this.subErrandPartToErrand,
                this.subErrandPartToErrandPartStatus);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartCombiner = this.CreateErrandPartCombiner();
            bool shouldForce = false;

            // Act
            await errandPartCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}

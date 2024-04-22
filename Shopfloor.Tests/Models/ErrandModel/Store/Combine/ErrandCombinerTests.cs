using NSubstitute;
using Shopfloor.Models.ErrandModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandCombinerTests
    {
        private ErrandToErrandPart subErrandToErrandPart;
        private ErrandToErrandStatus subErrandToErrandStatus;
        private ErrandToErrandType subErrandToErrandType;
        private ErrandToUser subErrandToUser;
        private ErrandToMachine subErrandToMachine;

        public ErrandCombinerTests()
        {
            this.subErrandToErrandPart = Substitute.For<ErrandToErrandPart>();
            this.subErrandToErrandStatus = Substitute.For<ErrandToErrandStatus>();
            this.subErrandToErrandType = Substitute.For<ErrandToErrandType>();
            this.subErrandToUser = Substitute.For<ErrandToUser>();
            this.subErrandToMachine = Substitute.For<ErrandToMachine>();
        }

        private ErrandCombiner CreateErrandCombiner()
        {
            return new ErrandCombiner(
                this.subErrandToErrandPart,
                this.subErrandToErrandStatus,
                this.subErrandToErrandType,
                this.subErrandToUser,
                this.subErrandToMachine);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandCombiner = this.CreateErrandCombiner();
            bool shouldForce = false;

            // Act
            await errandCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
